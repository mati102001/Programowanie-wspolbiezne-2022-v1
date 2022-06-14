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
        private readonly BallService service;


        private ObservableCollection<IBall> balls { get; set; }
        public BallFactory() : this(DataAbstractApi.CreateDataLayer()) { }
        public BallFactory(DataAbstractApi data) { 
            _data = data;
            service = new BallService(_data, 
            balls = new ObservableCollection<IBall>()); }

        public override double BoardWidth => _data.BoardWidth;

        public override double BoardHeight => _data.BoardHeight;

        public override IList CreateBalls(int count)
        {
            balls.Clear();
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
            _data.setUpCancellationToken();
            for (int i = 0; i < balls.Count; i++)
            {
                _data.callMovementTask(balls[i]);
                balls[i].PropertyChanged += PositionChange;
            }
            _data.CreateLoggingTask();     
        }
        public void PositionChange(object sender, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)sender;
            service.WallCollision(ball);
            service.BallCollision(ball);
           
        }

    }
}
