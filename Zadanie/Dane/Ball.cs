using System;

namespace Data
{
    public abstract class ApiData
    {
        public static ApiData CreateBall()
        {
            return new BallData();
        }
    }  
    internal class BallData : ApiData 
    {

    }
    
}
