using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class Ball : INotifyPropertyChanged
    {
        private double x;
        private double y;

        public event PropertyChangedEventHandler
           PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Ball(double x, double y)
        {
            this.x = x;
            this.y = y;
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
    }
}
