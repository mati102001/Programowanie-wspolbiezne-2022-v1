using Data;
using System;



namespace Logic
{
    internal class BallService
    {
        private readonly DataAbstractApi _data;

        public BallService(DataAbstractApi data)
        {
            _data = data;
        }

        public void WallCollision(IBall ball)
        {

            double diameter = ball.R;

            double right = 80 + _data.BoardWidth - diameter;

            double down = 10 + _data.BoardHeight - diameter;


            if (ball.X <= 80)
            {
                ball.X = 80;
                ball.XSpeed = -ball.XSpeed;
            }

            else if (ball.X >= right)
            {
                ball.X = right;
                ball.XSpeed = -ball.XSpeed;
            }
            if (ball.Y <= 10)
            {
                ball.Y = 10;
                ball.YSpeed = -ball.YSpeed;
            }

            else if (ball.Y >= down)
            {
                ball.Y = down;
                ball.YSpeed = -ball.YSpeed;
            }
        }

        public void BallCollision(IBall ball)
        {
            for (int i = 0; i < _data.Count(); i++)
            {
                IBall secondBall = _data.GetBall(i);
                if (ball == secondBall)
                {
                    continue;
                }

                if (IsCollision(ball, secondBall))
                {

                    double m1 = ball.Weight;
                    double m2 = secondBall.Weight;
                    double v1x = ball.XSpeed;
                    double v1y = ball.YSpeed;
                    double v2x = secondBall.XSpeed;
                    double v2y = secondBall.YSpeed;

                    double u1x = (m1 - m2) * v1x / (m1 + m2) + (2 * m2) * v2x / (m1 + m2);
                    double u1y = (m1 - m2) * v1y / (m1 + m2) + (2 * m2) * v2y / (m1 + m2);

                    double u2x = 2 * m1 * v1x / (m1 + m2) + (m2 - m1) * v2x / (m1 + m2);
                    double u2y = 2 * m1 * v1y / (m1 + m2) + (m2 - m1) * v2y / (m1 + m2);

                    ball.XSpeed = u1x;
                    ball.YSpeed = u1y;
                    secondBall.XSpeed = u2x;
                    secondBall.YSpeed = u2y;

                    return;

                }
            }
        }



        internal bool IsCollision(IBall a, IBall b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            double x1 = a.X + a.R / 2 + a.XSpeed;
            double y1 = a.Y + a.R / 2 + a.YSpeed;
            double x2 = b.X + b.R / 2 + b.XSpeed;
            double y2 = b.Y + b.R / 2 + b.YSpeed;

            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) <= (a.R / 2 + b.R / 2);
        }
    }
}
