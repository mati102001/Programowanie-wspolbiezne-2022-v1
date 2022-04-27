using System;
using System.Collections;

namespace Model
{
    public abstract class Model
    {
        public abstract IList Balls(int ballNumber);
        public static Model CreateApi()
        {
            return new ModelAPI();
        }
    }
}
