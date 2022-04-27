using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ModelApi : Model
    {
        public override IList Balls(int ballNumber)
        {
            return _logic.CreateBalls(ballNumber);
        }
    }
}
