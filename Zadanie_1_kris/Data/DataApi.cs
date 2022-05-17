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

        public abstract double getBallX(int i);

        public abstract double getBallY(int i);

        public abstract double getBallR(int i);

        public abstract void setBallX(int i, double x);

        public abstract void setBallY(int i, double y);

        public abstract void createBoard(int number);
        
        public static DataAbstractApi CreateDataLayer()
        {
            return new Board();
        }

        public abstract void Test(int number);
    }

    

    internal class Board : DataAbstractApi
    {
      
        private ObservableCollection<Balls> balls = new ObservableCollection<Balls>();
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

        public override void createBoard(int number)
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
                xSpeed = rand.Next(1, 10);
                ySpeed = rand.Next(1, 10);
                balls.Add(new Balls(i, x, y, 10, xSpeed, ySpeed, 50));
            }
        }

        public ObservableCollection<Balls> Balls => balls;

        public override IList GetAll()
        {
            return balls;
        }

        public override double getBallX(int i)
        {
            return balls[i].x;
        }

        public override double getBallY(int i)
        {
            return balls[i].y;
        }

        public override double getBallR(int i)
        {
            return balls[i].radius;
        }

        public override void setBallX(int i, double x)
        {
            balls[i].x = x;
        }

        public override void setBallY(int i, double y)
        {
            balls[i].y = y;
        }

        public override void Test(int number)
        {
            balls[number].CreateMovementTask(1, cancellationToken);
        }
    }
}
