using System;
using System.Collections;

partial class Program
{
    private static Dictionary<string, string> users = new Dictionary<string, string>
    {
        { "Oskar", "Pass123" }
    };

    static int attempts = 0;

    private static void CheckPassword(string user)
    {
        // Check attempts to make sure no hackers
        if (attempts >= 3)
        {
            Console.WriteLine("For mange forsøg!");
            return;
        }

        Console.Write("Password: ");
        string pass = Console.ReadLine();

        // Then check password
        if (users[user] == pass)
        {
            Console.WriteLine("Du er logget ind!");
        }
        else
        {
            attempts++;
            Console.WriteLine("Password er forkert!");
            CheckPassword(user);
        }
    }

    private static void LoginPrompt()
    {
        attempts = 0;
        Console.WriteLine("Login Prompt");
        Console.Write("Brugernavn: ");
        string user = Console.ReadLine();

        // Check if user exists
        if (users.ContainsKey(user))
        {
            // Prompt user for password after checking user exists first
            CheckPassword(user);
        }
        else
        {
            Console.WriteLine("Du har ikke adgang til systemet!");
        }
    }

    private static ArrayList navne = new ArrayList()
    {
        "William", "Oliver", "Noah", "Emil", "Victor",
        "Magnus", "Frederik", "Mikkel", "Lucas", "Alexander",
        "Oscar", "Mathias", "Sebastian", "Malthe", "Elias",
        "Christian", "Mads", "Gustav", "Villads", "Tobias"
    };

    private static void SorterNavne()
    {
        navne.Sort();
        DrengeNavne();
    }

    private static void VisNavne()
    {
        foreach (string name in navne)
        {
            Console.WriteLine(name);
        }
    }

    private static void SearchName()
    {
        Console.Clear();
        Console.Write("Skriv navnet eller noget af det: ");
        string input = Console.ReadLine();

        var matchingNames = navne.Cast<string>().Where(name => name.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0);

        Console.Clear();
        Console.WriteLine("Matchende navne:");
        foreach (var name in matchingNames)
        {
            Console.WriteLine(name);
        }
    }

    private static void AddNavn()
    {
        Console.Clear();
        Console.Write("Skriv navnet du vil tilføje: ");
        string input = Console.ReadLine();
        navne.Add(input);
        Console.WriteLine($"Navnet {input} er tilføjet.");
    }

    private static void RemoveNavn()
    {
        Console.Clear();
        Console.Write("Skriv navnet du vil fjerne: ");
        string input = Console.ReadLine();
        if (navne.Contains(input))
        {
            navne.Remove(input);
            Console.WriteLine($"Navnet {input} er fjernet.");
        }
        else
        {
            Console.WriteLine($"Navnet {input} findes ikke i listen.");
        }
    }

    private static void TilføjPigenavne()
    {
        string[] pigenavne = new string[]
        {
            "Emma", "Sofie", "Ida", "Laura", "Freja",
            "Anna", "Maja", "Clara", "Lærke", "Mathilde"
        };

        navne.AddRange(pigenavne);
        Console.WriteLine("Pigenavne er tilføjet til listen.");
    }

    private static void DrengeNavne()
    {
        Console.Clear();
        VisNavne();
        Console.WriteLine();
        Console.WriteLine("Vælg en handling:");
        Console.WriteLine("a - Sortere i alfabetisk orden");
        Console.WriteLine("s - Søge efter et navn med delvis søgning");
        Console.WriteLine("t - Tilføje et navn til listen");
        Console.WriteLine("f - Fjerne et navn fra listen");
        Console.WriteLine("p - Tilføje et array med pigenavne til listen");
        Console.WriteLine("q - Afslutte programmet");
        char type = char.Parse(Console.ReadLine().ToLower());

        bool on = true;

        switch (type)
        {
            case 'a':
                SorterNavne();
                break;
            case 's':
                SearchName();
                break;
            case 't':
                AddNavn();
                break;
            case 'f':
                RemoveNavn();
                break;
            case 'p':
                TilføjPigenavne();
                break;
            case 'q':
                on = false;
                break;
            default:
                Console.WriteLine("Ugyldigt valg.");
                break;
        }

        if (on)
        {
            DrengeNavne();
        }
    }

    static void Karakter()
    {
        Console.WriteLine();

        float[,] puntuations = new float[10, 10];
        for (int klasse = 0; klasse < 2; klasse++)
        {
            for (int elev = 0; elev < 10; elev++)
            {
                Console.Write("Karakteren for elev {0}, i klasse {1}: ", elev + 1, klasse + 1);
                try
                {
                    puntuations[klasse, elev] = Convert.ToSingle(Console.ReadLine());
                } catch (Exception ex) {
                    Console.WriteLine("Fejl i Input");
                    return;
                }
            }
        }

        Console.WriteLine();

        // Calculate og display average
        for (int klasse = 0; klasse < 2; klasse++)
        {
            float sum = 0;
            for (int elev = 0; elev < 10; elev++)
            {
                sum += puntuations[klasse, elev];
            }
            float average = sum / 10;
            Console.WriteLine("Gennemsnittet for klasse {0} er: {1}", klasse + 1, average);
        }
    }
    //
    private static float[] data = new float[1000]; // Array til at reservere plads til maksimalt 1000 elementer
    private static int dataCount = 0; // Tæller for at holde styr på antallet af indtastede elementer


    static void DataStyring()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Data Styring!");
            Console.WriteLine("\nHvad vil du?");
            Console.WriteLine("t - Tilføj nye data");
            Console.WriteLine("s - Se alle indtastede data");
            Console.WriteLine("f - Find et element ");
            Console.WriteLine("r - Se et resumé af statistikker: Antal data elementer, sum, gennemsnit, maksimum, minimum");
            Console.WriteLine("q - Afslut programmet");
            char type;
            try
            {
                type = char.Parse(Console.ReadLine().ToLower());
            } catch (Exception ex)
            {
                DataStyring();
                break;
            }

            switch (type)
            {
                case 't':
                    TilfoejData();
                    break;
                case 's':
                    SeAlleData();
                    break;
                case 'f':
                    FindElement();
                    break;
                case 'r':
                    SeStatistikker();
                    break;
                case 'q':
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    break;
            }
            if (running)
            {
                Console.ReadKey();

            }
        }
    }

    static void TilfoejData()
    {
        if (dataCount >= 1000)
        {
            Console.WriteLine("Kan ikke tilføje flere data, maksimal kapacitet nået.");
            return;
        }

        Console.Write("Indtast ny data (tal): ");
        float nyData;
        if (float.TryParse(Console.ReadLine(), out nyData))
        {
            data[dataCount] = nyData;
            dataCount++;
            Console.WriteLine("Data tilføjet!");
        }
        else
        {
            Console.WriteLine("Ugyldig indtastning. Prøv igen.");
        }
    }

    static void SeAlleData()
    {
        Console.WriteLine("Alle indtastede data:");
        for (int i = 0; i < dataCount; i++)
        {
            Console.WriteLine(data[i]);
        }
    }

    static void FindElement()
    {
        Console.Write("Indtast det element, du vil finde: ");
        float søgning;
        if (float.TryParse(Console.ReadLine(), out søgning))
        {
            bool found = false;
            for (int i = 0; i < dataCount; i++)
            {
                if (data[i] == søgning)
                {
                    Console.WriteLine($"Elementet {søgning} findes i listen på indeks {i}.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Elementet {søgning} blev ikke fundet i listen.");
            }
        }
        else
        {
            Console.WriteLine("Ugyldig indtastning. Prøv igen.");
        }
    }


    static void SeStatistikker()
    {
        if (dataCount > 0)
        {
            float sum = 0;
            float max = data[0];
            float min = data[0];

            for (int i = 0; i < dataCount; i++)
            {
                sum += data[i];
                if (data[i] > max) max = data[i];
                if (data[i] < min) min = data[i];
            }

            float average = sum / dataCount;

            Console.WriteLine("Statistikker:");
            Console.WriteLine($"Antal data elementer: {dataCount}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Gennemsnit: {average}");
            Console.WriteLine($"Maksimum: {max}");
            Console.WriteLine($"Minimum: {min}");
        }
        else
        {
            Console.WriteLine("Ingen data tilgængelig for statistik.");
        }
    }
    //
    static void ToDimmensionelt()
    {
        int[,] array =
        {
            { 1, 2, 3, 4, 5 },
            { 6, 7, 8, 9, 10 },
            { 11, 12, 13, 14, 15 }
        };

        for (int i = 0; i < array.GetLength(0); i++) // Loop through rows
        {
            for (int j = 0; j < array.GetLength(1); j++) // Loop through columns
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine(); // Move to the next line after each row
        }
    }
    //
    static void SimulerExcel()
    {
        // Opret et 3x3 array
        int[,] regneark = new int[3, 3];

        // Indtast værdier i regnearket
        for (int i = 0; i < regneark.GetLength(0); i++)
        {
            for (int j = 0; j < regneark.GetLength(1); j++)
            {
                Console.Write($"Indtast værdi for celle [{i},{j}]: ");
                regneark[i, j] = int.Parse(Console.ReadLine());
            }
        }

        // Vis regnearket
        Console.WriteLine("\nRegnearkets indhold:");
        for (int i = 0; i < regneark.GetLength(0); i++)
        {
            for (int j = 0; j < regneark.GetLength(1); j++)
            {
                Console.Write(regneark[i, j] + "\t");
            }
            Console.WriteLine();
        }

        // Vælg to celler at adde
        Console.WriteLine("\nVælg den første celle at adde:");
        Console.Write("Række (0-2): ");
        int r1 = int.Parse(Console.ReadLine());
        Console.Write("Kolonne (0-2): ");
        int k1 = int.Parse(Console.ReadLine());

        Console.WriteLine("Vælg den anden celle at adde:");
        Console.Write("Række (0-2): ");
        int r2 = int.Parse(Console.ReadLine());
        Console.Write("Kolonne (0-2): ");
        int k2 = int.Parse(Console.ReadLine());

        // Beregn og udskriv resultatet
        int sum;
        try
        {
            sum = regneark[r1, k1] + regneark[r2, k2]; 
        } catch (Exception ex)
        {
            Console.WriteLine("Fejl i mindst et af indexne.");
            SimulerExcel();
            return;
        }
        Console.WriteLine($"\nSummen af cellerne [{r1},{k1}] og [{r2},{k2}] er: {sum}");
    }

    static void Arrays()
    {
        // Define the menu options
        var options = new Dictionary<string, Action>
        {
            { "Login Prompt", LoginPrompt },
            { "Navne Data Styring", DrengeNavne },
            { "Navne Data Styring Udvidet", DrengeNavne },
            { "Karaktere", Karakter },
            { "Data Styring", DataStyring },
            { "Todimmensionelt Array", ToDimmensionelt },
            { "Simuler Excel", SimulerExcel }
        };

        // Create the menu
        WindowMenu menu = new WindowMenu(options, "Arrays");

        // Start the menu
        menu.Start();
    }
}
