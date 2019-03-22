namespace ComplexCalculator
{
    public interface IComplexNumber
    {
        double Im { get; set; }
        double Re { get; set; }

        double Arg();
        bool Compare(ComplexNumber X);
        ComplexNumber Conjugate();
        double[] GetParts();
        double Modulus();
        string Print();
    }
}