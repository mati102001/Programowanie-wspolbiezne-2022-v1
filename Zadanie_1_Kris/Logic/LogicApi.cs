using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicApi
    {
        ATimer timer;

        public abstract void WallBounce(Data.Object ball, int width, int height);

        public abstract event EventHandler CordinatesChanging;

        public abstract int boardWidth { get; }

        public abstract int boardHeight { get; }

        public abstract void Add();

        public abstract void Remove(Data.Object obiekt);

        public abstract Data.Object Get(int i);

        public abstract double GetX(int objectNumber);

        public abstract double GetY(int objectNumber);

        public abstract void Update(float miliseconds);

        public abstract int Count();

        public abstract void SetInterval(int miliseconds);

        public static LogicApi createLayer(int width, int height)
        {
            return new BallLogic(width, height);
        }

        public abstract void Start();

        public abstract void Stop();

        public abstract double GetBallRadius(Data.Object ball);

        internal class BallLogic : LogicApi
        {
            public override event EventHandler CordinatesChanging { add => timer.Tick += value; remove => timer.Tick -= value; }
            public override int boardWidth { get; }

            public override int boardHeight { get; }
            private Random r;
            private DataAPI dataLayer;
            public BallLogic(int width, int height)
            {
                dataLayer = DataAPI.Create();
                boardWidth = width;
                boardHeight = height;
                r = new Random();
                timer = ATimer.CreateWPFTimer();
                SetInterval(30);
                timer.Tick += (sender, args) => Update(timer.Interval.Milliseconds);
            }
            public override void Add()
            {
                int rad = 20;
                double x = r.Next(0, boardWidth - 2 * rad);
                double y = r.Next(0, boardHeight - 2 * rad);
                double speedX = r.Next(1, 10);
                double speedY = r.Next(1, 10);

                Data.Object ball = DataAPI.CreateBall(x, y, speedX, speedY, rad);
                dataLayer.Add(ball);
            }

            public override void WallBounce(Data.Object ball, int width, int height)
            {
                // Średnica kuli.
                double diameter = dataLayer.GetBallRadius(ball) * 2;
                // Prawa ściana nie licząc średnicy kuli.
                double right = width - diameter;
                // Prawa Dolna nie licząc średnicy kuli.
                double down = height - diameter;

                // Prawo.
                if (ball.X < 0)
                {
                    ball.X = -ball.X;
                    ball.SpeedX = -ball.SpeedX;
                }
                // Lewo.
                else if (ball.X > right)
                {
                    ball.X = right - (ball.X - right);
                    ball.SpeedX = -ball.SpeedX;
                }

                // Góra.
                if (ball.Y < 0)
                {
                    ball.Y = -ball.Y;
                    ball.SpeedY = -ball.SpeedY;
                }
                // Dół.
                else if (ball.Y > down)
                {
                    ball.Y = down - (ball.Y - down);
                    ball.SpeedY = -ball.SpeedY;
                }
            }

            public override void Update(float miliseconds)
            {
                for (int i = 0; i < dataLayer.Count(); i++)
                {
                    dataLayer.Get(i).Move(miliseconds/100);

                    WallBounce(dataLayer.Get(i), boardWidth, boardHeight);
                }
            }

            public override int Count()
            {
                return dataLayer.Count();
            }

            public override Data.Object Get(int i)
            {
                return dataLayer.Get(i);
            }

            public override double GetX(int objectNumber)
            {
                return dataLayer.Get(objectNumber).X;
            }

            public override double GetY(int objectNumber)
            {
                return dataLayer.Get(objectNumber).Y;
            }

            public override void Remove(Data.Object obiekt)
            {
                dataLayer.Remove(obiekt);
            }
            public override void Start()
            {
                timer.Start();
            }

            public override void Stop()
            {
                timer.Stop();
            }

            public override void SetInterval(int miliseconds)
            {
                timer.Interval = TimeSpan.FromMilliseconds(miliseconds);
            }

            public override double GetBallRadius(Data.Object ball)
            {
                return dataLayer.GetBallRadius(ball);
            }
            
        }
        
        
    }
}
