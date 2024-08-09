partial class Program
{
    static void MetoderOgFiler()
    {
        try
        {
            // Define the menu options with corresponding functions
            var options = new Dictionary<string, Action>
            {
                { "Opgave 1 skriv 'han skød først' til StarWars.txt", Opgave1 },
                { "Opgave 2 læs StarWars.txt fil", Opgave2 },
                { "Opgave 3 slet StarWars.txt fil", Opgave3 },
                { "Opgave 4 lav Droids folder", Opgave4 },
                { "Opgave 5 slet Droids folder", Opgave5 },
                { "Opgave 6 enumerate filer i en mappe", Opgave6 },
                { "Opgave 7 læs fra fil med stream", Opgave7 },
                { "Opgave 8 skriv til fil med 'actors' liste", Opgave8 },
                { "SYSTEM | Fil og mappe manipulation", Opgave9 }
            };

            // Create the menu
            WindowMenu menu = new WindowMenu(options, "MetoderOgFiler");

            // Start the menu
            menu.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen: " + ex.Message);
        }
    }

    private static void Opgave9()
    {
        // Define the menu options with corresponding functions
        var options = new Dictionary<string, Action>
            {
                { "Add file", Opgave1 },
                { "Delete file", Opgave2 },
                { "Display files", Opgave3 },
                { "Add folder", Opgave4 },
                { "Search file", Opgave5 },
                { "Other stuff ex JPEG", Opgave6 },
            };

        // Create the menu
        WindowMenu menu = new WindowMenu(options, "SYSTEM | Fil og mappe manipulation");

        // Start the menu
        menu.Start();
    }

    // Split out each case into its own method
    private static void Opgave1()
    {
        Console.Clear();
        File.WriteAllText(@"Files\StarWars.txt", "han skød først");
        Console.WriteLine("Opgave 1: Teksten 'han skød først' er skrevet til StarWars.txt");
        PromptForKeyPress();
    }

    private static void Opgave2()
    {
        Console.Clear();
        if (File.Exists(@"Files\StarWars.txt"))
        {
            var content = File.ReadAllText(@"Files\StarWars.txt");
            Console.WriteLine(@"Opgave 2: StarWars.txt: " + content);
        }
        else
        {
            Console.WriteLine("Opgave 2: Der er ikke nogen StarWars.txt fil");
        }
        PromptForKeyPress();
    }

    private static void Opgave3()
    {
        Console.Clear();
        if (File.Exists(@"Files\StarWars.txt"))
        {
            File.Delete(@"Files\StarWars.txt");
            Console.WriteLine("Opgave 3: StarWars.txt filen er slettet");
        }
        else
        {
            Console.WriteLine("Opgave 3: Der er ikke nogen StarWars.txt fil");
        }
        PromptForKeyPress();
    }

    private static void Opgave4()
    {
        Console.Clear();
        Directory.CreateDirectory(@"Files\Droids");
        File.WriteAllText(@"Files\Droids\R2D2.txt", "beep bop");
        Console.WriteLine("Opgave 4: Droids folderen er oprettet og R2D2.txt er tilføjet");
        PromptForKeyPress();
    }

    private static void Opgave5()
    {
        Console.Clear();
        if (Directory.Exists(@"Files\Droids"))
        {
            Directory.Delete(@"Files\Droids", true);
            Console.WriteLine("Opgave 5: Droids folderen er slettet");
        }
        else
        {
            Console.WriteLine("Opgave 5: Droids folderen findes ikke.");
        }
        PromptForKeyPress();
    }

    private static void Opgave6()
    {
        Console.Clear();
        if (Directory.Exists(@"Files\Droids"))
        {
            Console.WriteLine("Opgave 6: Alle filer i 'Droids' folder:");
            string[] files = Directory.GetFiles(@"Files\Droids");
            foreach (string fil in files)
            {
                Console.WriteLine(fil);
            }
        }
        else
        {
            Console.WriteLine("Opgave 6: Droids folderen findes ikke.");
        }
        PromptForKeyPress();
    }

    private static void Opgave7()
    {
        Console.Clear();
        using (FileStream file = new FileStream(@"Files\Movies.txt", FileMode.Open))
        using (StreamReader reader = new StreamReader(file))
        {
            Console.WriteLine("Opgave 7: Movies from 'Movies.txt':");
            while (!reader.EndOfStream)
            {
                string movie = reader.ReadLine();
                Console.WriteLine(movie);
            }
        }
        PromptForKeyPress();
    }

    private static void Opgave8()
    {
        Console.Clear();
        string[] actors = { "Harrison Ford", "Carrie Fisher", "Mark Hamill" };

        using (FileStream file = new FileStream(@"Files\Movies.txt", FileMode.Append))
        using (StreamWriter writer = new StreamWriter(file))
        {
            foreach (string actor in actors)
            {
                writer.WriteLine(actor);
            }
        }
        Console.WriteLine("Opgave 8: Skrevet aktører til 'Movies.txt'.");
        PromptForKeyPress();
    }

    // Method to clear the console and wait for a key press before proceeding
    private static void PromptForKeyPress()
    {
        Console.WriteLine("\nTryk på en knap for at fortsætte...");
        Console.ReadKey();
        Console.Clear();
    }
}
