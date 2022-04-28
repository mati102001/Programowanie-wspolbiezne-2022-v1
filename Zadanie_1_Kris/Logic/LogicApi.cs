using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicApi
    {
        private int boardWidth;

        private int boardHeight;

        private Random r;

        private DataAPI dataLayer;

        public abstract void Add();

        public abstract void Remove(Data.Object obiekt);

        public abstract Data.Object Get(int i);

        public abstract double GetX(int objectNumber);

        public abstract double GetY(int objectNumber);

        public abstract int Count();

        public LogicApi createLayer(int width, int height, DataAPI data = default(DataAPI))
        {
            return new BallLogic(width, height, data);
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        public double GetBallRadius(Data.Object ball)
        {
            return dataLayer.GetBallRadius(ball);
        }

        internal class BallLogic : LogicApi
        {
            public BallLogic(int width, int height, DataAPI dataLayerAPI)
            {
                dataLayer = dataLayerAPI;
                boardWidth = width;
                boardHeight = height;
                r = new Random();
            }
            public override void Add()
            {
                int rad = 20;
                double x = r.Next(0, boardWidth - 2 * rad);
                double y = r.Next(0, boardHeight - 2 * rad);
         
                Data.Object ball = DataAPI.CreateBall(x, y, rad);
                dataLayer.Add(ball);
            }

            public override int Count()
            {
                return dataLayer.Count();
            }

            public override Data.Object Get(int i)
            {
                return dataLayer.Get(i);
            }

            public override double GetX(int objectNumber)
            {
                return dataLayer.Get(objectNumber).X;
            }

            public override double GetY(int objectNumber)
            {
                return dataLayer.Get(objectNumber).Y;
            }

            public override void Remove(Data.Object obiekt)
            {
                dataLayer.Remove(obiekt);
            }
        }
        
        
    }
}
