using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class BallFactory : LogicAPI
    {
        private readonly DataAbstractApi _data;
        public BallFactory() : this(DataAbstractApi.CreateDataLayer()) { }
        public BallFactory(DataAbstractApi data) { _data = data; }
        Random rand = new Random();
        private List<Task> tasks = new List<Task>();
        private IList balls;

        public override IList CreateBalls(int number)
        {
            tasks.Clear();
            _data.createBoard(number);
            balls = _data.GetAll();
            return _data.GetAll();
        }

        public int Tasks
        {
            get => tasks.Count;
        }
        public override void Start()
        {
            for (var i = 0; i < balls.Count; i++)
            {
                //wstrzymanie glownego watku
                Thread.Sleep(6);
                //kolejkuje określoną pracę do uruchomienia w puli wątków
                tasks.Add(Task.Run(() => Update(i-1)));
            }
        }

        public async void Update(int b)
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
            x_new = rand.Next(140, _data.BoardWidth -10);
            y_new = rand.Next(20, _data.BoardHeight -10); 
            while (true)
            {
                diffrence_x = _data.getBallX(b) - x_new;
                diffrence_y = _data.getBallY(b) - y_new;
                diffrence_x2 = x_new - _data.getBallX(b);
                diffrence_y2 = y_new - _data.getBallX(b);
                diffrence_x = Math.Abs(diffrence_x);
                diffrence_y = Math.Abs(diffrence_y);
                //d = sqrt((x1-x2)^2+(y1-y2)^2)
                d = Math.Sqrt((diffrence_x * diffrence_x) + (diffrence_y * diffrence_y));
                //zeby bylo szybciej trzeba pomniejszyc ruch
                move_x = diffrence_x2 / d;
                move_y = diffrence_y2 / d;
                for (int i = 0; i < d; i++)
                {
                    await Task.Delay(10);
                    _data.setBallX(b, _data.getBallX(b) + move_x);
                    _data.setBallY(b, _data.getBallY(b) + move_y);
                }
                //nextDouble zwraca losową liczbę zmiennoprzecinkową
                x_new = rand.Next(140, _data.BoardWidth - 10);
                y_new = rand.Next(20, _data.BoardHeight - 10);
            }
        }

    }
}