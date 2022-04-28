using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class Object
    {
        public double X { get; set; }

        public double Y { get; set; }

        internal class Ball : Object
        {
            public double BallRadius { get; set; }

            public Ball(double x, double y, double radius)
            {
                X = x;
                Y = y;
                BallRadius = radius;
            }
        }
    }
}
