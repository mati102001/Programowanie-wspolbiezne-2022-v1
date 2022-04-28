using System;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateDataLayer()
        {
            return new DataApi();
        }
    }

    internal class DataApi : DataAbstractApi
    {
        //Nothing
    }
}
