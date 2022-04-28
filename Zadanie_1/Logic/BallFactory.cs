using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Logic
{
    internal class BallFactory : LogicAPI
    {
        public override IList CreateBalls(int number, double XLimit, double YLimit)
        {
            Random random = new Random();
            ObservableCollection<Ball> ballList = new ObservableCollection<Ball>();

            double x, y, r;
            
            for (int i = 0; i < number; i++)
            {
                r = 30;
                x = random.Next(10, (int)(XLimit - r) - 1) + random.NextDouble();
                y = random.Next(10, (int)(YLimit - r) - 1) + random.NextDouble();

                ballList.Add(new Ball(x, y));
            }
            return ballList;
        }
    }
}
