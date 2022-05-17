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
    internal class Balls : INotifyPropertyChanged
    {
        public double x { get; set; }

        public double y { get; set; }

        private int Id { get; }

        private double xSpeed { get; }

        private double ySpeed { get; }

        private double weight { get; }

        public double radius {get; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Balls(int Id, double x, double y, double radius, double xSpeed, double ySpeed, double weight)
        {
            this.Id = Id;
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

        public void Move(double interval)
        {
            X += xSpeed * interval;
            Y += ySpeed * interval;
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
                    Move(interval);
                    OnPropertyChanged();
                }
                stopwatch.Stop();

                await Task.Delay((int)(interval - stopwatch.ElapsedMilliseconds), cancellationToken);
            }
        }

        private readonly Stopwatch stopwatch = new Stopwatch();
    }
}
