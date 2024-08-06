// Prompt

using System.Reflection;

partial class Program
{
    static Random random = new Random();

    static private bool on = true;

    static void Main()
    {
        // Find path til 'Opgaver' folder for at opnå automatisk opdatering af liste til menu
        string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string opgaverPath = Path.Combine(projectRoot, "Opgaver");

        // Create List
        string[] scriptFiles = Directory.GetFiles(opgaverPath, "*.cs");
        List<string> Opgaver = new List<string>();
        foreach (string scriptFile in scriptFiles)
        {
            Opgaver.Add(Path.GetFileNameWithoutExtension(scriptFile));
        }
        // 
        int selectedIndex = 0;


        // Start Main Loop
        do
        {
            Console.Clear();

            // Draw List
            for (int i = 0; i < Opgaver.Count; i++)
            {
                if (i == selectedIndex)
                {
                    // Highlight the selected item
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(Opgaver[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(Opgaver[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Brug op og ned pile...");

            // Get user input
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            // Update selection based on arrow keys
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (selectedIndex > 0)
                {
                    selectedIndex--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (selectedIndex < Opgaver.Count - 1)
                {
                    selectedIndex++;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                // Handle the selection
                string selectedOpgave = Opgaver[selectedIndex];

                MethodInfo method = typeof(Program).GetMethod(selectedOpgave, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != null)
                {
                    Console.Clear();
                    method.Invoke(null, null);
                    Console.WriteLine("");
                    Console.WriteLine("Tryk på en knap for at gå videre...");
                    Console.ReadKey();
                }
            }
        } while (on);
    }
}
