using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball
    {
       /* public event PropertyChangedEventHandler
           PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

        public  double X { get; set; }
        public  double Y { get; set; }

        public Ball(double x, double y)
        {
            X = x;
            Y = y;
        }

       
    }
}
