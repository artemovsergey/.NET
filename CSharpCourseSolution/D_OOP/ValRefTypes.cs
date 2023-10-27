using System;
using System.Collections.Generic;
using System.Text;

namespace D_OOP
{
    public struct EvilStruct
    {
        public int X;
        public int Y;

        public readonly PointRef PointRef;

        public EvilStruct(int x, int y)
        {
            X = x;
            Y = y;
            PointRef = new PointRef();
        }

        public void Do()
        {
            //PointRef = new PointRef();
        }
    }

    public struct PointVal
    {
        public int X;
        public int Y;

        public void LogValues()
        {
            Console.WriteLine($"X={X}; Y={Y}");
        }
    }

    public class PointRef
    {
        public int X;
        public int Y;

        public void LogValues()
        {
            Console.WriteLine($"X={X}; Y={Y}");
        }
    }
}
