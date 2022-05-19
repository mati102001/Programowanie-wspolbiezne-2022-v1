using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public int BoardWidth { get; internal set; }

        public int BoardHeight { get; internal set; }

        public abstract IList GetAll();

        public abstract int Count();

        public abstract void createBalls(int number);

        public abstract IBall GetBall(int i);

        public static DataAbstractApi CreateDataLayer()
        {
            return new Board();
        }
    }

    

    internal class Board : DataAbstractApi
    {
      
        private ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
        Random rand = new Random();
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        internal Board()
        {
            BoardWidth = 640;
            BoardHeight = 320;
        }

        public override int Count()
        {
            return balls.Count;
        }

        public override void createBalls(int number)
        {
            balls.Clear();
            double x;
            double y;
            double xSpeed;
            double ySpeed;
            for (int i = 0; i < number; i++)
            {
                x = rand.Next(140, BoardWidth - 10);
                y = rand.Next(20, BoardHeight - 10);
                xSpeed = rand.Next(1,5);
                ySpeed = rand.Next(1,5);
                balls.Add(new Ball(x, y, 10, xSpeed, ySpeed, 50));
            }
        }

        public ObservableCollection<Ball> Balls => balls;

        public override IList GetAll()
        {
            return balls;
        }

        public override IBall GetBall(int index)
        {
            return balls[index];
        }

    }
}
