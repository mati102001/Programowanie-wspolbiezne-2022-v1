using System;
using System.Collections;
using Data;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;

namespace Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateBallAPI() => new BallFactory();

        public abstract IList CreateBalls(int number);

        public abstract void Start();

        public abstract double BoardWidth { get; }

        public abstract double BoardHeight { get; }
    }

    internal class BallFactory : LogicAPI
    {
        private readonly DataAbstractApi _data;
        private readonly Mutex mutex = new Mutex();
        private readonly BallService service;
        private readonly ConcurrentQueue<IBall> queue;

        public BallFactory() : this(DataAbstractApi.CreateDataLayer()) { }
        public BallFactory(DataAbstractApi data) { _data = data; service = new BallService(_data, balls = new ObservableCollection<IBall>()); }

        public override double BoardWidth => _data.BoardWidth;

        public override double BoardHeight => _data.BoardHeight;

        private CancellationTokenSource cancellationTokenSource;

        private CancellationToken cancellationToken;

        private ObservableCollection<IBall> balls { get; set; }

        public override IList CreateBalls(int count)
        { 

            for (int i = 0; i < count; i++)
            {
                bool contain = true;
                bool licz;


                while (contain)
                {
                    balls.Add(_data.createBall());
                    licz = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (balls[i].X <= balls[j].X + balls[j].R && balls[i].X + balls[i].R >= balls[j].X)
                        {
                            if (balls[i].Y <= balls[j].Y + balls[j].R && balls[i].Y + balls[i].R >= balls[j].Y)
                            {

                                licz = true;
                                balls.Remove(balls[i]);
                                break;
                            }
                        }
                    }
                    if (!licz)
                    {
                        contain = false;
                    }
                }
            }
            return balls;
        }

        public override void Start()
        {
            if(cancellationTokenSource != null)
                cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].CreateMovementTask(10, cancellationToken,queue);
                balls[i].PropertyChanged += PositionChange;
            }
                
        }

        

        public void PositionChange(object sender, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)sender;
            mutex.WaitOne();
            if (ball == null)
            {
                mutex.ReleaseMutex();
                return;
            }
            service.WallCollision(ball);
            service.BallCollision(ball);
            mutex.ReleaseMutex();
        }

    }
}
