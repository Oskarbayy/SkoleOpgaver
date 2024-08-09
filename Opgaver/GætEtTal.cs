
partial class Program
{
    static int gæt = 0;

    static int hemmeligtTal = random.Next(1, 11);

    static void GætEtTal()
    {
        try
        {
            // Prompt
            Console.Write("Gæt et tal: ");
            double brugerGæt = double.Parse(Console.ReadLine());
            gæt += 1;

            // Calculation
            if (brugerGæt < hemmeligtTal)
            {
                Console.WriteLine("For lavt! Prøv igen.");
                GætEtTal();
            }
            else if (brugerGæt > hemmeligtTal)
            {
                Console.WriteLine("For højt! Prøv igen.");
                GætEtTal();
            }
            else
            {
                Console.WriteLine($"Tillykke! Du gættede det rigtige tal på {gæt} forsøg.");

                if (gæt <= 5)
                {
                    Console.WriteLine("Fantastisk! Du gættede meget hurtigt! :))");
                }
                else if (gæt <= 10)
                {
                    Console.WriteLine("Godt gået! Du gættede det indenfor en passende tid. (skrald)");
                }
                else
                {
                    Console.WriteLine("Det tog dig mange forsøg. :(");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            GætEtTal();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
