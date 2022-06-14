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

        public abstract Task CreateLoggingTask();

        public abstract void setUpCancellationToken();

        public abstract void callMovementTask(IBall ball);

        public abstract void AppendObjectToJSONFile(string filename, string newJsonObject);

        public static DataAbstractApi CreateDataLayer()
        {
            return new Board();
        }
    }


    internal class Board : DataAbstractApi
    {
        Random rand = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string logPath = "Log.json";

        private bool newSession;

        private readonly Stopwatch stopwatch;

        private CancellationToken cancellationToken;

        private CancellationTokenSource cancellationTokenSource;

        private ConcurrentQueue<IBall> logQueue;

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
            cancellationToken = new CancellationToken();
            logQueue = new ConcurrentQueue<IBall>();
        }

        public override IBall createBall()
        {
            double x;
            double y;
            double xSpeed;
            double ySpeed;

            x = rand.Next(140, (int)BoardWidth - 10);
            y = rand.Next(20, (int)BoardHeight - 10);
            xSpeed = rand.Next(-5, 5)*rand.NextDouble();
            ySpeed = rand.Next(-5, 5)*rand.NextDouble();

            IBall ball = new Ball(x, y, 20, xSpeed, ySpeed, 50);
            return ball;
        }

        public override void callMovementTask(IBall ball)
        {
            ball.CreateMovementTask(16, cancellationToken, logQueue);
        }

        public override void setUpCancellationToken()
        {
            if (cancellationTokenSource != null)
                cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
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

        public override Task CreateLoggingTask()
        {
            return CallLogger();
        }

        internal async Task CallLogger()
        {
            if (File.Exists(logPath) && newSession)
            {
                newSession = false;
                File.Delete(logPath);
            }
            string diagnostics;
            string date;
            string log;
            while (!cancellationToken.IsCancellationRequested)
            {
                stopwatch.Reset();
                stopwatch.Start();
                logQueue.TryDequeue(out IBall logObject);
                if (logObject != null)
                {
                    if (!logQueue.IsEmpty)
                    {
                        diagnostics = JsonSerializer.Serialize(logObject);
                        date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                        log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, diagnostics) + "}";

                        File.AppendAllText(logPath, log);

                    }
                    stopwatch.Stop();
                    await Task.Delay(10);
                }
            }
        }

        public override void AppendObjectToJSONFile(string filename, string newJsonObject)
        {
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("[]");
            }

            string content;
            using (StreamReader sr = File.OpenText(filename))
            {
                content = sr.ReadToEnd();
            }

            content = content.TrimEnd();
            content = content.Remove(content.Length - 1, 1);

            if (content.Length == 1)
            {
                content = String.Format("{0}\n{1}\n]\n", content.Trim(), newJsonObject);
            }
            else
            {
                content = String.Format("{0},\n{1}\n]\n", content.Trim(), newJsonObject);
            }

            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.Write(content);
            }
        }
    }
}
