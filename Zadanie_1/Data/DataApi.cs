using System.Text.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract double BoardWidth { get;  internal set; }

        public abstract double BoardHeight { get;  internal set; }

        public abstract IBall createBall();

        public static DataAbstractApi CreateDataLayer()
        {
            return new Board();
        }
    }


    internal class Board : DataAbstractApi
    {
        private ObservableCollection<Ball> balls = new ObservableCollection<Ball>();

        Random rand = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string logPath = "Log.json";

        private bool newSession;

        private readonly Stopwatch stopwatch;

        private bool stop;

        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private double boardWidth;
        private double boardHeight;

        internal Board()
        {
            BoardWidth = 640;
            BoardHeight = 320;
            newSession = true;
            stopwatch = new Stopwatch();
        }

        public override IBall createBall()
        {
            double x;
            double y;
            double xSpeed;
            double ySpeed;

            x = rand.Next(140, (int)BoardWidth - 10);
            y = rand.Next(20, (int)BoardHeight - 10);
            xSpeed = 0.1 + rand.NextDouble();
            ySpeed = 0.1 + rand.NextDouble();

            IBall ball = new Ball(x, y, 20, xSpeed, ySpeed, 50);
            return ball;
        }

        public override double BoardWidth
        {
            get => boardWidth;
            internal set
            {
                boardWidth = value;
                OnPropertyChanged();
            }
        }

        public override double BoardHeight
        {
            get => boardHeight;
            internal set
            {
                boardHeight = value;
                OnPropertyChanged();
            }
        }

        internal async Task CallLogger(ConcurrentQueue<IBall> logQueue)
        {
            if (File.Exists(logPath) && newSession)
            {
                newSession = false;
                File.Delete(logPath);
            }
            string diagnostics;
            string date;
            string log;
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                logQueue.TryDequeue(out IBall logObject);
                if (logObject != null)
                {
                    diagnostics = JsonSerializer.Serialize(logObject);
                    date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                    log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, diagnostics) + "}";

                    lock (this)
                    {
                        File.AppendAllText(logPath, log);
                    }
                }
                stopwatch.Stop();
                await Task.Delay((int)(stopwatch.ElapsedMilliseconds));
            }
        }
    }
}
