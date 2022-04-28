using System;
using System.Collections.Generic;
using static Data.Object;

namespace Data
{
    
    public abstract class DataAPI
    {
        public abstract void Add(Object Obiekt);

        public abstract Object Get(int i);

        public abstract bool Remove(Object Obiekt);

        public abstract int Count();

        public static DataAPI Create()
        {
            return new ObjectBoard();
        }

        public double GetBallRadius(Object ball)
        {
            return ((Ball)ball).BallRadius;
        }

        public static Object CreateBall(double x, double y, double speedX, double speedY, double r)
        {
            return new Ball(x, y, r, speedX, speedY);
        }

        internal class ObjectBoard : DataAPI
        {
            internal ObjectBoard()
            {
                balls = new List<Object>();
            }

            public override void Add(Object Obiekt)
            {
                balls.Add(Obiekt);
            }

            public override int Count()
            {
                return balls.Count;
            }

            public override Object Get(int i)
            {
                return (Object)balls[i];
            }

            public override bool Remove(Object Obiekt)
            {
                return balls.Remove(Obiekt);
            }

            private readonly List<Object> balls;
        }
    }
}
