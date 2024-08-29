
partial class Program
{
    static void ValutaOmregner()
    {
        try
        {
            // Prompt
            Console.Write("Danske Kroner: ");
            double DKK = double.Parse(Console.ReadLine());

            // Constants
            const double DKKtoUSD = 0.15f;
            const double DKKtoSEK = 1.56f;

            // Calculation
            double amountUSD = DKK * DKKtoUSD;
            double amountSEK = DKK * DKKtoSEK;

            // Result with 2 decimal places
            Console.WriteLine("USD: " + Math.Round(amountUSD, 2));
            Console.WriteLine("SEK: " + Math.Round(amountSEK, 2));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, prøv igen");
            ValutaOmregner();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
