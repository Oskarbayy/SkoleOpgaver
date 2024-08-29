
partial class Program
{
    static void Liste()
    {
        try
        {
            // Generate List
            List<int> listeB = new List<int>();

            Console.WriteLine("Lav en liste listeB bestående af de lige tal mellem 1 - 20:");
            for (int i = 1; i <= 20; i++)
            {
                if (i % 2 == 0)
                {
                    listeB.Add(i);
                    Console.Write(i + " ");
                }
            }

            Console.WriteLine("\n\nFjern alle tal som 3 går op i:");

            for (int i = 0; i < listeB.Count; i++)
            {
                // debug Console.WriteLine("\n" + listeB[i] + "%" + 3 + "== 0 =" + (listeB[i] % 3 == 0));
                if (listeB[i] % 3 == 0)
                {
                    listeB.RemoveAt(i);
                    i -= 1;
                } else
                {
                    Console.Write(listeB[i] +" ");
                }
            }

            Console.WriteLine("\n\nHvor mange elementer er der nu i listen?");
            Console.WriteLine(listeB.Count);

            Console.WriteLine("\nIndsæt værdien 17 på plads nr. 3");
            listeB[3] = 17;
            foreach (int i in listeB)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine("\n\nLav en ny liste der består af listeB’s elementer – men i omvendt rækkefølge:");
            // use AsEnumerable to make it use the LINQ interface which reverse
            // method returns a new list and not changes the list directly
            List<int> listeA = listeB.AsEnumerable().Reverse().ToList();
            Console.Write("listeA: ");
            foreach (int i in listeA)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen, "+ex);
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
