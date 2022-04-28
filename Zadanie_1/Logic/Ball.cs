using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler
           PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public  float X { get; set; }
        public  float Y { get; set; }

        public Ball(float x, float y)
        {
            X = x;
            Y = y;
        }

       
    }
}
