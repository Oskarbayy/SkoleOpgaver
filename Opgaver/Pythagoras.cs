partial class Program
{
    static void Pythagoras()
    {
        try
        {
            // Prompt
            Console.Write("side a længde: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("side b længde: ");
            double b = double.Parse(Console.ReadLine());

            Console.WriteLine();

            // Calculation
            double c2 = Math.Pow(a, 2) + Math.Pow(b, 2);
            double c = Math.Sqrt(c2);

            // Result
            Console.WriteLine("C længde: " + c);
            Console.WriteLine();

            // Check a or b highest
            if (a > b)
            {
                Console.WriteLine("a er højere end b");
            }
            else
            {
                Console.WriteLine("b er højere end a");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            Pythagoras();
        }
    }
}
