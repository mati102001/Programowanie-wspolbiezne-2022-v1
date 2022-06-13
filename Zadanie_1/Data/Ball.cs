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
        double R { get;}
        double Weight { get; }
        double X { get; }
        double Y { get;}
        double XSpeed { get;}
        double YSpeed { get;}
        void ChangeSpeed(double xSpeed, double ySpeed);
        void Move();
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
            private set
            {
                if (value.Equals(x)) return;
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double Y
        {
            get => y;
            private set
            {
                if (value.Equals(y)) return;
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        public double R
        {
            get => radius;
           private set
            {
                if (value.Equals(radius)) return;
                radius = value;
                OnPropertyChanged(nameof(R));

            }
        }

        public double Weight { get => weight; }

        public double XSpeed { get => xSpeed; private set {
                if (value.Equals(xSpeed))
                {
                    return;
                }

                xSpeed = value;
            } 
        }
        public double YSpeed { get => ySpeed; private set {
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

        public void ChangeSpeed(double xSpeed, double ySpeed)
        {
          
                XSpeed = xSpeed;
                YSpeed = ySpeed;
        
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
