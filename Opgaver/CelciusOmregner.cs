
partial class Program
{
    static void CelciusOmregner()
    {
        try
        {
            // Prompt
            Console.Write("Celsius: ");
            double Celcius = double.Parse(Console.ReadLine());

            // Calculation
            double Reamur = Celcius * 0.8;
            double Fahrenheit = Celcius * 1.8 + 32;

            // Result
            Console.WriteLine("Reamur: " + Reamur);
            Console.WriteLine("Fahrenheit: " + Fahrenheit);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            CelciusOmregner();
        }
    }
}
