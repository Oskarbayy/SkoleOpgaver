
partial class Program
{
    static void LigningsLøseren()
    {
        try
        {
            // Prompt
            Console.Write("Input Ligning: ");
            string Ligning = Console.ReadLine(); // En ligning

            // Calculation


            // Result

        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            LigningsLøseren();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
