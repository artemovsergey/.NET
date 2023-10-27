using System;
using System.Collections.Generic;
using System.Text;

namespace D_OOP.Homeworks
{
    public class Complex
    {
        public double Imaginary { get; private set; }
        public double Real { get; private set; }

        public Complex(double imaginary, double real)
        {
            Imaginary = imaginary;
            Real = real;
        }

        private Complex()
        {
        }

        public Complex Plus(Complex other)
        {
            var complex = new Complex();
            complex.Imaginary = other.Imaginary + Imaginary;
            complex.Real = other.Real + Real;

            return complex;
        }

        public Complex Minus(Complex other)
        {
            var complex = new Complex();
            complex.Imaginary = other.Imaginary - Imaginary;
            complex.Real = other.Real - Real;

            return complex;
        }
    }
}
