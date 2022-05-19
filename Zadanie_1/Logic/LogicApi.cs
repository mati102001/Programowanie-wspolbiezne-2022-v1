using System;
using System.Collections;
using Data;
using System.Threading;
using System.ComponentModel;

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

        public BallFactory() : this(DataAbstractApi.CreateDataLayer()) { }
        public BallFactory(DataAbstractApi data) { _data = data; service = new BallService(_data); }

        private IList balls => _data.GetAll();

        public override double BoardWidth => _data.BoardWidth;

        public override double BoardHeight => _data.BoardHeight;

        private CancellationTokenSource cancellationTokenSource;

        private CancellationToken cancellationToken;

        public override IList CreateBalls(int number)
        {
            _data.createBalls (number);
            IList ballsTemp = _data.GetAll();
            for (int i = 0; i < _data.Count(); i++) {
                _data.GetBall(i).PropertyChanged += PositionChange;
            }
            return ballsTemp;
        }

        public override void Start()
        {
            if(cancellationTokenSource != null)
                cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            for (int i = 0; i < balls.Count; i++)
                _data.GetBall(i).CreateMovementTask(10, cancellationToken);
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
