using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class Object
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double SpeedX { get; set; }

        public double SpeedY { get; set; }

        public abstract void Move(double miliseconds);

        internal class Ball : Object
        {
            public double BallRadius { get; set; }

            public Ball(double x, double y, double speedX, double speedY, double radius)
            {
                X = x;
                Y = y;
                SpeedX = speedX;
                SpeedY = speedY;
                BallRadius = radius;
            }

            public override void Move(double milliseconds)
            {
                X += SpeedX * milliseconds;
                Y += SpeedY * milliseconds;
            }
        }
    }
}
