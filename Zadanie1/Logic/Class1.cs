using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAbstractApi {
        public abstract int screenWidth { get; }
        public abstract int screenHeight { get; }
        public abstract void CreateBalls(int number);
        public abstract List<Ball> GetBalls();
        public static LogicAbstractApi CreateApi(int width, int height)
        {
            return new LogicApi(width, height);
        }
    }

    public class LogicApi : LogicAbstractApi
    {
        public override int screenWidth { get; }
        public override int screenHeight { get; }
       
        private List<Ball> _balls = new List<Ball>();

        public List<Ball> Balls { get { return _balls; } }

        public override void CreateBalls(int number)
        {
            Random rnd = new Random();
            
            for(int i = 0; i < number; i++)
            {
                int randomR = rnd.Next(1, 20);
                float randomX = rnd.Next(randomR, (int) (screenWidth - randomR));
                float randomY = rnd.Next(randomR, (int) (screenWidth - randomR));

                _balls.Add(new Ball(randomX, randomY, randomR));
            }
        }
        public LogicApi(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            _balls = new List<Ball>();
        }
        public override List<Ball> GetBalls()
        {
            return _balls;
        }
    }



}
