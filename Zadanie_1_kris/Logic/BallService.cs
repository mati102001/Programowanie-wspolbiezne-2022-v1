using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Logic
{
    internal class BallService
    {
        private readonly DataAbstractApi _data;

        public BallService(DataAbstractApi data)
        {
            _data = data;
        }

        /*public void WallBounce(int i, int width, int height)
        {

            // Średnica kuli.
            double diameter = _data.getBallR(i);
            // Prawa ściana nie licząc średnicy kuli.
            double right = width - diameter;
            // Prawa Dolna nie licząc średnicy kuli.
            double down = height - diameter;

            // Prawo.
            if (_data.getBallX(i) <= 0)
            {
                //Back(ball, right, down);
                _data.getBallX(i) = -ball.X;
                ball.SpeedX = -ball.SpeedX;
            }
            // Lewo.
            else if (ball.X >= right)
            {
                //Back(ball, right, down);
                ball.X = right - (ball.X - right);
                ball.SpeedX = -ball.SpeedX;
            }

            // Góra.
            if (ball.Y < 0)
            {
                //Back(ball, right, down);
                ball.Y = -ball.Y;
                ball.SpeedY = -ball.SpeedY;
            }
            // Dół.
            else if (ball.Y > down)
            {
                //Back(ball, right, down);
                ball.Y = down - (ball.Y - down);
                ball.SpeedY = -ball.SpeedY;
            }
        }*/
    }
}
