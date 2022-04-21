using System;

namespace Model
{
    public abstract class BallAbstract
    {
        public abstract float BallX { get; }
        public abstract float BallY { get; }
        public abstract int BallR { get; }
    }

    public class Ball : BallAbstract
    {
        private Logic.Ball ball;
        public override float BallX
        {
            get => ball.x;
        }
        public override float BallY
        {
            get => ball.y;
        }
        public override int BallR
        {
            get => ball.r;
        }

        public Ball(Logic.Ball ball)
        {
            this.ball = ball;
        }
    }
}
