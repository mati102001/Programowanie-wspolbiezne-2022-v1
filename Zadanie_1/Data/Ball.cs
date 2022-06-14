using System;
using System.Collections.Concurrent;
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
        double R { get; }
        double Weight { get; }
        double X { get; }
        double Y { get; }
        double XSpeed { get; }
        double YSpeed { get; }
        void ChangeSpeed(double xSpeed, double ySpeed);
        void Move(double time, ConcurrentQueue<IBall> queue);
        Task CreateMovementTask(int interval, CancellationToken cancellationToken, ConcurrentQueue<IBall> queue);

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

        private readonly object ballLock = new object();

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
            
            get
            {
                lock (ballLock) return x;
            }
            
            private set
            {
                if (value.Equals(x)) return;
                x = value;
            }
        }
        public double Y
        {
            get
            {
                lock(ballLock) return y;
            }
            private set
            {
                if (value.Equals(y)) return;
                y = value;
            }
        }
        public double R
        {
            get
            {
                lock (ballLock) return radius;
            }
           private set
            {
                if (value.Equals(radius)) return;
                radius = value;
                OnPropertyChanged(nameof(R));

            }
        }

        public double Weight { get => weight; }

        public double XSpeed { get
            {
                lock (ballLock) return xSpeed;  
            }
            private set {
                if (value.Equals(xSpeed))
                {
                    return;
                }

                xSpeed = value;
            } 
        }
        public double YSpeed { get
            {
                lock (ballLock) return ySpeed;
            }
            private set {
                if (value.Equals(ySpeed))
                {
                    return;
                }
                ySpeed = value;
            }  
        }

        delegate int AddOperation(int x, int y);

        public void Move(double time, ConcurrentQueue<IBall> queue)
        {

            lock (ballLock)
            {
                ImmutableObjectAttribute(X, Y, time);                    
                OnPropertyChanged(nameof(X));
                OnPropertyChanged(nameof(Y));
                SaveRequest(queue);
            }
        }

        private void ImmutableObjectAttribute(double x, double y, double time)
        {
                X += xSpeed * time;
                Y += ySpeed * time;        
        }

        public void ChangeSpeed(double xSpeed, double ySpeed)
        {
            lock (ballLock)
            {
                XSpeed = xSpeed;
                YSpeed = ySpeed;
            }
        }

        public Task CreateMovementTask(int interval, CancellationToken cancellationToken, ConcurrentQueue<IBall> queue)
        {
            return Run(interval, cancellationToken, queue);
        }

        public void SaveRequest(ConcurrentQueue<IBall> queue)
        {
            queue.Enqueue(new Ball(X, Y, R, XSpeed, YSpeed, Weight));
        }

        private async Task Run(int interval, CancellationToken cancellationToken, ConcurrentQueue<IBall> queue)
        {
            
            while (!cancellationToken.IsCancellationRequested)
            {
                stopwatch.Reset();
                stopwatch.Start();
                if (!cancellationToken.IsCancellationRequested)
                {
                    Move((interval - stopwatch.ElapsedMilliseconds)/10 , queue); 
                    
                }
                stopwatch.Stop();

                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds), cancellationToken);
            }
        }


        private readonly Stopwatch stopwatch = new Stopwatch();
    }
}
