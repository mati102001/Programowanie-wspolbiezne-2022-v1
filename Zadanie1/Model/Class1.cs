using System;
using System.Collections.ObjectModel;
using Logic;
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
    public abstract class ModelAbstractApi
    {
        protected LogicAbstractApi _logic;
        public abstract ObservableCollection<Ball> GetBalls();
        public abstract void CreateBalls(int number);
        public static ModelAbstractApi CreateApi(int width, int height)
        {
            return new ModelApi(width, height);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        private ObservableCollection<Ball> _balls;
        public override ObservableCollection<Ball> GetBalls()
        {
            return _balls;
        }
        public override void CreateBalls(int number)
        {
            _logic.CreateBalls(number);
            _balls.Clear();
            var convertedBalls = _logic.GetBalls().ConvertAll(ball => new Ball(ball));
            foreach (var ball in convertedBalls)
            {
                _balls.Add(ball);
            }
        }
        internal ModelApi(int width, int height)
        {
            _logic = LogicAbstractApi.CreateApi(width, height);
            _balls = new ObservableCollection<Ball>();
        }
    }
}
