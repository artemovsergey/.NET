using System;
using System.Collections.Generic;
using System.Text;

namespace D_OOP
{
    public interface IShape
    {
        int CalcSquare();
    }

    public class Rect : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int CalcSquare()
        {
            return Width * Height;
        }
    }

    public class Square : IShape
    {
        public int SideLength { get; set; }

        public int CalcSquare()
        {
            return SideLength * SideLength;
        }
    }

    //public static class AreaCalculator
    //{
    //    public static int CalcSquare(Square square)
    //    {
    //        return square.Height * square.Height;
    //    }

    //    public static int CalcSquare(Rect rect)
    //    {
    //        return rect.Width * rect.Height;
    //    }
    //}
}
