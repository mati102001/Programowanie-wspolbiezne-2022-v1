using System;

namespace ClassLibrary1
{
    public class Calculator
    {

        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Divide(double x, double y)
        {
            if (y == 0)
                throw new ArgumentException("Nie można dzielić przez zero");
            return x / y;
        }

        public double Condemnation(double x, int y)
        {
            double result = 1;
            if (y < 0)
            {
                if (x == 0)
                {
                    throw new ArgumentException("Nie można dzielić przez zero");
                }
                for (int i = 0; i > y; i--)
                {
                    result *= x;
                }
                result = 1 / result;
            }
            else
            {
                for (int i = 0; i < y; i++)
                {
                    result *= x;
                }
            }
            return result;
        }
    }
}
 