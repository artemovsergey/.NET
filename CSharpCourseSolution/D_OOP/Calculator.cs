using System;
using System.Collections.Generic;
using System.Text;

namespace D_OOP
{
    public static class Calculator
    {
        public static bool TryDivide(double divisible, double divisor, out double result)
        {
            result = 0;
            if(divisor == 0)
            {
                return false;
            }
            result = divisible / divisor;
            return true;
        }

        public static double Average(int[] numbers)
        {
            int sum = 0;
            
            foreach(int n in numbers)
            {
                sum += n;
            }
            return (double)sum / numbers.Length;
        }

        public static double Average2(params int[] numbers)
        {
            int sum = 0;

            foreach (int n in numbers)
            {
                sum += n;
            }
            return (double)sum / numbers.Length;
        }

        public static double CalcTriangleSquare(double ab, double bc, double ac)
        {
            double p = (ab + bc + ac) / 2;

            return Math.Sqrt(p * (p - ab) * (p - bc) * (p - ac));            
        }

        public static double CalcTriangleSquare(double b, double h)
        {
            return 0.5 * b * h;
        }

        public static double CalcTriangleSquare(double ab, double ac, int alpha, bool isInRadians = false)
        {
            if (isInRadians)
            {
                return 0.5 * ab * ac * Math.Sin(alpha);
            }
            else
            {
                double rads = alpha * Math.PI / 180;
                return 0.5 * ab * ac * Math.Sin(rads);
            }            
        }

        //public static double CalcTriangleSquare(double ab, double ac, int alpha)
        //{
        //    double rads = alpha * Math.PI / 180;
        //    return 0.5 * ab * ac * Math.Sin(rads);
        //}
    }
}
