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
        public abstract int BoardWidth { get; set; }

        public abstract int BoardHeight { get; set; }

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
        private int boardWidth;
        private int boardHeight;

        internal Board()
        {
            boardWidth = 640;
            boardHeight = 320;
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

        public override int BoardWidth {
            get => boardWidth; set
            {
                if (value.Equals(BoardHeight)) return;
                boardHeight = value;
                OnPropertyChanged(nameof(BoardWidth));

            }
        }
        public override int BoardHeight
        {
            get => boardHeight; set
            {
                if (value.Equals(BoardHeight)) return;
                boardHeight = value;
                OnPropertyChanged(nameof(BoardHeight));

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
