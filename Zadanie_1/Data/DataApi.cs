using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract double BoardWidth { get;  set; }

        public abstract double BoardHeight { get;  set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private double boardWidth;
        private double boardHeight;

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
                x = rand.Next(140, (int) BoardWidth - 10);
                y = rand.Next(20, (int) BoardHeight - 10);
                xSpeed = 0.1 + rand.NextDouble();
                ySpeed = 0.1 + rand.NextDouble();
                balls.Add(new Ball(x, y, 20, xSpeed, ySpeed, 50));
            }
        }

        public ObservableCollection<Ball> Balls => balls;

        public override double BoardWidth
        {
            get => boardWidth; set
            {
                boardWidth = value;
                OnPropertyChanged();
            }
        }
        public override double BoardHeight { get => boardHeight; set
            {
                boardHeight = value;
                OnPropertyChanged();
            }
        }

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
