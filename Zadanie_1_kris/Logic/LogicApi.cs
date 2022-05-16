using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateBallAPI() => new BallFactory();
        public abstract IList CreateBalls(int number, double XLimit, double YLimit);
        public abstract void Start();
    }
}
