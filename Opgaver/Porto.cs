/*
 * Porto
Du skal skrive et lille program som kan finde portoen for et brev. Som input til programmet skal være brevets størrelse, vægt og hvilket land brevet skal sendes til og outputtet skal være den pågældende pris / porto.

Du kan finde mere http://www.postnord.dk

Udvid dit program til også at håndtere pakker.
*/


partial class Program
{
    // Dictionary for brev porto
    const int letterPrice = 25; // DKK
    const int letterMaxLength = 60; // cm
    const int letterMaxSize = 90; // h+l+b cm
    const int letterMaxWeight = 2000; // g
    static Dictionary<double, double> brevPorto = new Dictionary<double, double>
    {
        // weight, proto
        { 250, 25 },
        { 500, 50 }
    };

    // Dictionary for pakke porto
    const int pakkePrice = 55;
    const int pakkePriceOut = 207;
    const int pakkeMaxLength = 120; // cm
    static Dictionary<double, double> pakkePorto = new Dictionary<double, double>
    {
        // weight, proto
        { 5000, 10 },
        { 10000, 30 },
        { 15000, 70 },
        { 25000, 134 }
    };

    // Porto pakke outside country (sweden)
    static Dictionary<double, double> pakkePortoOut = new Dictionary<double, double>
    {
        // weight, proto
        { 5000, 94 },
        { 10000, 279 },
        { 15000, 372 },
        { 20000, 541 }

    };

    static void Porto()
    {
        double AddProto(double price, Dictionary<double, double> dict, double weight)
        {
            foreach (KeyValuePair<double, double> kvp in dict)
            {
                double curWeight = kvp.Key;
                double curProto = kvp.Value;

                if (weight < curWeight)
                {
                    price = price + curProto;
                    return price;
                }
            }

            return price;
        }

        try
        {
            // Prompts
            Console.Write("Er det et brev eller en pakke? (b/p): ");
            char type = char.Parse(Console.ReadLine().ToLower());

            Console.Write("Højde (cm): ");
            double h = double.Parse(Console.ReadLine());

            Console.Write("Bredde (cm): ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Længde (cm): ");
            double l = double.Parse(Console.ReadLine());

            Console.Write("Vægt (g): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Land: ");
            string country = Console.ReadLine().ToLower();

            // Calculation
            double price = 0;

            // Check if too big size
            bool error = false;
            if (type == 'b')
            {
                price = letterPrice;
                if (h + b + l > letterMaxSize)
                {
                    Console.WriteLine("Brevet er for stort");
                    error = true;
                }
                if (l > letterMaxLength)
                {
                    Console.WriteLine("Brevet er for langt");
                    error = true;
                }

                // Calculate price
                price = AddProto(price, brevPorto, weight);

                if (country != "danmark")
                {
                    price *= 2;
                }

            } else if (type == 'p') {
                if (l > pakkeMaxLength)
                {
                    Console.WriteLine("Pakken er for lang");
                    error = true;
                }
                if (country == "danmark")
                {
                    price = pakkePrice;
                    // Calculate price
                    price = AddProto(price, pakkePorto, weight);

                }
                else // Outside of demark
                {
                    price = pakkePriceOut;
                    // Calculate price
                    price = AddProto(price, pakkePortoOut, weight);
                }

            }
            if (error)
            {
                Porto();
                return;
            }

            Console.WriteLine("Price: " + price);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            Porto();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
