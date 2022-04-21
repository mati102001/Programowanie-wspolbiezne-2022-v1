using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Ball
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public int r { get; private set; }

        public Ball(float x, float y, int r)
         {
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }

}
