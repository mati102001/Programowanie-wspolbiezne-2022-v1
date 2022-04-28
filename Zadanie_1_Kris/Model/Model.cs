using System;
using Logic;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Model
{
   public class ModelApi
    {
        private Random r;

        private Canvas canvas;

        private Rectangle rectangle;

        public int StartBalls { get => startBalls; set => startBalls = value; }

        public Canvas Canvas { get => canvas; set => canvas = value; }

        public ModelApi(int width, int height, LogicApi api = null)
        {
            //logicAPI = api ?? logicAPI.createLayer(width, height);
            ellipses = new List<Ellipse>();
            Canvas = new Canvas {Width=width, Height = height, Background= Brushes.Yellow };
            Canvas.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            Canvas.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            rectangle = new Rectangle {Width=width, Height=height, Stroke = Brushes.Black, StrokeThickness = 1 };
            r = new Random();
        }
        public void CreateBall()
        {
            //logicAPI.Add();
            //int ballNumer = logicAPI.Count() - 1;
            //double radius = logicAPI.GetBallRadius(logicAPI.Get(ballNumer));
            //double x = logicAPI.GetX(ballNumer);
            //double y = logicAPI.GetY(ballNumer);
            double radius = 20;
            double x = r.Next(0, 450);
            double y = r.Next(0, 300);
            Ellipse newEllipse = new Ellipse { Width = radius * 2, Height = radius * 2, Fill = Brushes.Red, StrokeThickness = 2, Stroke = Brushes.Black };
            ellipses.Add(newEllipse);

            Canvas.SetLeft(newEllipse, x);
            Canvas.SetTop(newEllipse, y);

            Canvas.Children.Add(newEllipse);
        }

        public void CreateBalls()
        {
            for (int i = 0; i < startBalls; i++)
                CreateBall();
        }

        public void Start() => logicAPI.Start();

        public void Stop() => logicAPI.Stop();


        private readonly LogicApi logicAPI = default(LogicApi);

        private readonly List<Ellipse> ellipses;

        private int startBalls = 1;

    }
}
