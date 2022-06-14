using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    internal class BallData
    {
        public string date { get; }

        private double dataX { get; } 

        private double dataY { get; }

        private double dataXspeed { get; }

        private double dataYspeed { get; }

        public BallData(double dataX, double dataY, double dataXspeed, double dataYspeed)
        {
            this.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            this.dataX = dataX;
            this.dataY = dataY;
            this.dataXspeed = dataXspeed;
            this.dataYspeed = dataYspeed;
        }
    }
}
