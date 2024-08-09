
partial class Program
{
    static void TerningKastet()
    {
        Random random = new Random();
        int terningskast = random.Next(1, 7);

        switch(terningskast)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Du slog en etter");
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Du slog en to'er");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Du slog en tre'er");
                break;
            case 4:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Du slog en fire");
                break;
            case 5:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Du slog en femer");
                break;
            case 6:
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Du slog en sekser");
                break;
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
