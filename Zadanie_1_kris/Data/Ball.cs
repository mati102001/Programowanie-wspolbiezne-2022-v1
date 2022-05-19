using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        double R { get; set; }
        double Weight { get; }

        double X { get; set; }
        double Y { get; set; }
        double XSpeed { get; set; }
        double YSpeed { get; set; }

        void Move(double interval);
        Task CreateMovementTask(int interval, CancellationToken cancellationToken);

    }

    internal class Ball : IBall
    {
        private double x;

        private double y;

        private double xSpeed;

        private double ySpeed;

        private double weight;

        public double radius;

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Ball(double x, double y, double radius, double xSpeed, double ySpeed, double weight)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.weight = weight;
        }
        
        public double X
        {
            get => x;
            set
            {
                if (value.Equals(x)) return;
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double Y
        {
            get => y;
            set
            {
                if (value.Equals(y)) return;
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        public double R
        {
            get => radius;
            set
            {
                if (value.Equals(radius)) return;
                y = value;
                OnPropertyChanged(nameof(R));

            }
        }

        public double Weight { get => weight; }

        public double XSpeed { get => xSpeed; set {
                if (value.Equals(xSpeed))
                {
                    return;
                }

                xSpeed = value;
            } 
        }
        public double YSpeed { get => ySpeed; set {
                if (value.Equals(ySpeed))
                {
                    return;
                }
                ySpeed = value;
            }  
        }

        public void Move()
        {
            X += xSpeed ;
            Y += ySpeed ;
        }

        public Task CreateMovementTask(int interval, CancellationToken cancellationToken)
        {
            return Run(interval, cancellationToken);
        }

        private async Task Run(int interval, CancellationToken cancellationToken)
        {
            
            while (!cancellationToken.IsCancellationRequested)
            {
                stopwatch.Reset();
                stopwatch.Start();
                if (!cancellationToken.IsCancellationRequested)
                {
                    Move();
                }
                stopwatch.Stop();

                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds), cancellationToken);
            }
        }

        private readonly Stopwatch stopwatch = new Stopwatch();
    }
}
