using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ModelView
{
    public class ViewModelWindow : MainViewModelBase
    {
        private int _ballNumber;
        private IList _balls;
        private Canvas canvas;
        private int width = 500;
        private int height = 300;
        private Rectangle rectangle;
        public Canvas Canvas { get => canvas; set => canvas = value; }

        public ViewModelWindow()
        {
            _balls = null;
            canvas = new Canvas();
            canvas.Background = System.Windows.Media.Brushes.Yellow;
            canvas.Width = width;
            canvas.Height = height;
            canvas.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            canvas.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            rectangle = new Rectangle();
            canvas.Children.Add(rectangle);
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Stroke = System.Windows.Media.Brushes.Black;
            rectangle.StrokeThickness = 1;
            Canvas.SetLeft(rectangle, 0);
            Canvas.SetTop(rectangle, 0);    
            Ellipse ellipse = new Ellipse();
            canvas.Children.Add(ellipse);
            ellipse.Width = 20;
            ellipse.Height = 20;
            ellipse.Stroke = System.Windows.Media.Brushes.Black;
            ellipse.StrokeThickness = 1;
            ellipse.Fill = System.Windows.Media.Brushes.Red;
            Canvas.SetLeft(ellipse, 50);
            Canvas.SetTop(ellipse, 50);
        }

        public int BallNumber
        {
            get => _ballNumber;
            set
            {
                if (value.Equals(_ballNumber))
                    return;
                if (value < 0)
                    value = 0;
                if (value > 2000)
                    value = 2000;
                _ballNumber = value;
                OnPropertyChanged();
            }
        }
       
    }
}
