namespace ComplexCalculator
{
    public interface ICalculator
    {
        double NewIm { get; set; }
        double NewRe { get; set; }

        ComplexNumber Add(ComplexNumber x, ComplexNumber y);
        ComplexNumber Div(ComplexNumber x, ComplexNumber y);
        double[] GetLastParts();
        ComplexNumber Mul(ComplexNumber x, ComplexNumber y);
        ComplexNumber Pow(ComplexNumber x, int n);
        ComplexNumber Sub(ComplexNumber x, ComplexNumber y);
    }
}