using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexCalculator
{
    public class ComplexNumber : IComplexNumber
    {
        public double Re { get; set; }
        public double Im { get; set; }

        public ComplexNumber(double re, double im)
        {
            this.Re = re;
            this.Im = im;
        }

        public ComplexNumber(double re) : this(re, 0.0)
        {
        }

        public ComplexNumber() : this(0.0, 0.0)
        {
        }

        public double Modulus()
        {
            return Math.Sqrt(Math.Pow(Re, 2.0) + Math.Pow(Im, 2.0));
        }

        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(Re, (Im * (-1.0)));
        }

        public double Arg()
        {
            return Math.Atan2(Im, Re);
        }

        public String Print()
        {
            double[] parts = GetParts();
            if (Im == 0) return String.Format("{0}", parts[0]);
            if (Re == 0) return String.Format("{0}i", parts[1]);
            if (Im < 0.0) return String.Format("{0}{1}i", parts[0], parts[1]);
            return String.Format("{0}+{1}i", parts[0], parts[1]);
        }

        public bool Compare(ComplexNumber X)
        {
            if (Re == X.Re && Im == X.Im) return true;
            return false;
        }

        public static bool IsZero(ComplexNumber x)
        {
            if (x.Re == 0.0 && x.Im == 0.0) return true;
            else return false;
        }

        public double[] GetParts()
        {
            return new double[] { Math.Round(Re, 5), Math.Round(Im, 5) };
        }
    }
}
