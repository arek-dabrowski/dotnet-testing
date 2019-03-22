using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexCalculator
{
    public class Calculator : ICalculator
    {
        public double NewRe { get; set; }
        public double NewIm { get; set; }

        public Calculator(IComplexNumber x)
        {
            double[] parts = x.GetParts();
            NewRe = parts[0];
            NewIm = parts[1];
        }

        public Calculator(double re, double im)
        {
            NewRe = re;
            NewIm = im;
        }

        public Calculator() : this(0.0, 0.0)
        {
        }

        public ComplexNumber Add(ComplexNumber x, ComplexNumber y)
        {
            NewRe = x.Re + y.Re;
            NewIm = x.Im + y.Im;

            return new ComplexNumber(NewRe, NewIm);
        }

        public ComplexNumber Sub(ComplexNumber x, ComplexNumber y)
        {
            NewRe = x.Re - y.Re;
            NewIm = x.Im - y.Im;

            return new ComplexNumber(NewRe, NewIm);
        }

        public ComplexNumber Mul(ComplexNumber x, ComplexNumber y)
        {
            NewRe = x.Re * y.Re - x.Im * y.Im;
            NewIm = x.Re * y.Im + x.Im * y.Re;

            return new ComplexNumber(NewRe, NewIm);
        }

        public ComplexNumber Div(ComplexNumber x, ComplexNumber y)
        {
            if (ComplexNumber.IsZero(y)) throw new DivideByZeroException("You can't divide by zero!");

            double denominator = Math.Pow(y.Re, 2.0) + Math.Pow(y.Im, 2.0);

            NewRe = (x.Re * y.Re + x.Im * y.Im) / denominator;
            NewIm = (x.Im * y.Re - x.Re * y.Im) / denominator;

            return new ComplexNumber(NewRe, NewIm);
        }

        public ComplexNumber Pow(ComplexNumber x, int n)
        {
            if (DeMoivreFormulaDateTest())
            {
                double xArg = n * x.Arg();
                ComplexNumber modZ = new ComplexNumber(Math.Pow(x.Modulus(), n));
                ComplexNumber newX = new ComplexNumber(Math.Cos(xArg), Math.Sin(xArg));

                return Mul(modZ, newX);
            }
            else return null;
        }

        private bool DeMoivreFormulaDateTest()
        {
            if (DateTime.Now.Year < 1730) return false;
            else return true;
        }

        public double[] GetLastParts()
        {
            return new double[] { NewRe, NewIm };
        }
    }
}
