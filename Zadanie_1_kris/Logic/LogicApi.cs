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
        public abstract void Stop();
    }

    internal class BallFactory : LogicAPI
    {
        private readonly DataAbstractApi _data;
        private readonly Mutex mutex = new Mutex();
        private readonly BallService service;

        public BallFactory() : this(DataAbstractApi.CreateDataLayer()) { }
        public BallFactory(DataAbstractApi data) { _data = data; service = new BallService(_data); }

        private IList balls => _data.GetAll();

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
           
        }

        public void WallCollision(IBall ball)
        {

            double diameter = ball.Radius;

            double right = _data.BoardWidth - diameter;

            double down = _data.BoardHeight - diameter;


            if (ball.X <= 0)
            {
                ball.X = - ball.X;
                ball.XSpeed = -ball.XSpeed;
            }

            else if (ball.X >= right)
            {
                ball.X = right - (ball.X - right);
                ball.XSpeed = -ball.XSpeed;
            }
            if (ball.Y <= 0)
            {
                ball.Y =  -ball.Y;
                ball.YSpeed = -ball.YSpeed;
            }

            else if (ball.Y >= down)
            {
                ball.Y = down - (ball.Y - down);
                ball.YSpeed = -ball.YSpeed;
            }
        }

        public void BallBounce(IBall ball)
        {
            for (int i = 0; i < _data.Count(); i++)
            {
                IBall secondBall = _data.GetBall(i);
                if (ball == secondBall)
                {
                    continue;
                }

                if (Collision(ball, secondBall))
                {

                    double m1 = ball.Weight;
                    double m2 = secondBall.Weight;
                    double v1x = ball.XSpeed;
                    double v1y = ball.YSpeed;
                    double v2x = secondBall.XSpeed;
                    double v2y = secondBall.YSpeed;

                    if (Math.Abs(m1 - m2) < 0.1)
                    {
                        (ball.XSpeed, secondBall.XSpeed) = (secondBall.XSpeed, ball.XSpeed);
                        (ball.YSpeed, secondBall.YSpeed) = (secondBall.YSpeed, ball.YSpeed);
                    }
                    else
                    {
                        double u1x = (m1 - m2) * v1x / (m1 + m2) + (2 * m2) * v2x / (m1 + m2);
                        double u1y = (m1 - m2) * v1y / (m1 + m2) + (2 * m2) * v2y / (m1 + m2);

                        double u2x = 2 * m1 * v1x / (m1 + m2) + (m2 - m1) * v2x / (m1 + m2);
                        double u2y = 2 * m1 * v1y / (m1 + m2) + (m2 - m1) * v2y / (m1 + m2);

                        ball.XSpeed = u1x;
                        ball.YSpeed = u1y;
                        secondBall.XSpeed = u2x;
                        secondBall.YSpeed = u2y;
                    }
                    return;

                }



            }

        }


        public override void Stop()
        {
            
            for (int i = 0; i < balls.Count; i++)
                _data.GetBall(i).CreateMovementTask(30, cancellationToken);
        }

        internal bool Collision(IBall a, IBall b)
        {
            if (a == null || b == null)
            {
                return false;
            }

            return Distance(a, b) <= (a.Radius + b.Radius );
        }

        internal double Distance(IBall a, IBall b)
        {
            double x1 = a.X + a.Radius  + a.XSpeed;
            double y1 = a.Y + a.Radius  + a.YSpeed;
            double x2 = b.X + b.Radius  + b.XSpeed;
            double y2 = b.Y + b.Radius  + b.YSpeed;

            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
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
            WallCollision(ball);
            BallBounce(ball);
            mutex.ReleaseMutex();
        }

    }
}
