
partial class Program
{
    static void Rumfanget()
    {
        try
        {
            // Prompts
            Console.Write("Højde: ");
            double h = double.Parse(Console.ReadLine());

            Console.Write("Bredde: ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Længde: ");
            double l = double.Parse(Console.ReadLine());

            //
            Console.WriteLine();

            // Calculation
            double Rumfanget = h * b * l;

            // Result
            Console.WriteLine("Rumfang: " + Rumfanget);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            Rumfanget();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}