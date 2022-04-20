using System;

namespace Logic
{
    public class Ball
    {
        private int x { get; set; }
        private int y { get; set; }
        private int r { get; set; }

        public Ball(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }

}
