using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    // internal class BallFactory : LogicAPI
    // {
    /*public override IList CreateBalls(int number, double XLimit, double YLimit)
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
    }*/
    public class BallFactory : LogicAPI
    {
        Random rand = new Random();
        private List<Task> tasks = new List<Task>();
        private ObservableCollection<Ball> balls = new ObservableCollection<Ball>();

        public override IList CreateBalls(int number, double XLimit, double YLimit)
        {
            balls.Clear();
            tasks.Clear();
            double x;
            double y;
            for (int i = 0; i < number; i++)
            {
                x = rand.Next(10, 604);
                y = rand.Next(10, 384);
                balls.Add(new Ball(x, y));
            }
            return balls;
        }
        public ObservableCollection<Ball> Balls => balls;

        public int Tasks
        {
            get => tasks.Count;
        }
        public override void Start()
        {
            double x;
            double y;
            for (var i = 0; i < balls.Count; i++)
            {
                Ball ball = balls[i];
                //wstrzymanie glownego watku
                Thread.Sleep(6);
                //kolejkuje określoną pracę do uruchomienia w puli wątków
                tasks.Add(Task.Run(() => Update(ball)));
            }
        }

        public async void Update(Ball ball)
        {
            double x_new;
            double y_new;
            double move_x;
            double move_y;
            double d;
            double diffrence_x;
            double diffrence_y;
            double diffrence_x2;
            double diffrence_y2;
            x_new = rand.Next(10, 604) + rand.NextDouble();
            y_new = rand.Next(10, 384) + rand.NextDouble();
            while (true)
            {
                diffrence_x = ball.X - x_new;
                diffrence_y = ball.Y - y_new;
                diffrence_x2 = x_new - ball.X;
                diffrence_y2 = y_new - ball.Y;
                diffrence_x = Math.Abs(diffrence_x);
                diffrence_y = Math.Abs(diffrence_y);
                //d = sqrt((x1-x2)^2+(y1-y2)^2)
                d = Math.Sqrt((diffrence_x * diffrence_x) + (diffrence_y * diffrence_y));
                //zeby bylo szybciej trzeba pomniejszyc ruch
                move_x = diffrence_x2 / d;
                move_y = diffrence_y2 / d;
                for (int i = 0; i < d; i++)
                {
                    await Task.Delay(20);
                    ball.X += move_x;
                    ball.Y += move_y;
                }
                //nextDouble zwraca losową liczbę zmiennoprzecinkową
                x_new = rand.Next(10, 604) + rand.NextDouble();
                y_new = rand.Next(10, 384) + rand.NextDouble();
            }
        }


    }
    //   }
}