using System;

namespace Data
{
    public abstract class ApiData
    {
        public abstract int Radius { get; }
        public static ApiData CreateBall()
        {
            return new BallData();
        }
    }  
    internal class BallData : ApiData 
    {
        public override int Radius => 10;
    }
    
}
