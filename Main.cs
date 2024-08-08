using System.Diagnostics;
using System.Reflection;

partial class Program : Buffer
{
    static Random random = new Random();
    static private bool on = true;

    static void Main(string[] args)
    {
        int itemsPerPage = 15;
        int currentPage = 0;

        // Get the base directory
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;

        // Find path til 'Opgaver' folder for at opnå automatisk opdatering af liste til menu
        string opgaverPath = Path.Combine(projectDirectory, "Opgaver");

        // Create List
        string[] scriptFiles = Directory.GetFiles(opgaverPath, "*.cs");
        List<string> Opgaver = new List<string>();
        foreach (string scriptFile in scriptFiles)
        {
            Opgaver.Add(Path.GetFileNameWithoutExtension(scriptFile));
        }

        int selectedIndex = 0;

        //
        int maxPage = (int)Math.Ceiling((double)Opgaver.Count / itemsPerPage) - 1;


        // Try and Draw Box to the buffer
        DrawBox(0, 0, 20, 60);
        AddText(1, 26, "Opgaver");

        // Start Main Loop
        do
        {
            Console.CursorVisible = false;
            // Clear the portion of the buffer where the list is drawn
            for (int i = 2; i < 18; i++)
            {
                AddText(i, 2, new string(' ', 56));
            }

            // Update Page Text
            string pageText = $"Side {currentPage+1} / {maxPage+1}"; // + 1 to make more sense when its front end feedback
            string noText = "              ";

            AddText(1, 58 - noText.Length, noText);
            AddText(1, 58 - pageText.Length, pageText);

            // Calculate the starting row for centering the text
            int startRow = 10 - (itemsPerPage / 2);


            for (int i = itemsPerPage*currentPage; i < Opgaver.Count && i<itemsPerPage*(currentPage+1); i++)
            {
                string displayText = Opgaver[i];
                ConsoleColor color = ConsoleColor.Black;
                if (i == selectedIndex)
                {
                    color = ConsoleColor.White;
                }

                // Calculate horizontal padding to center the text
                int padding = (56 - displayText.Length) / 2;
                AddText(startRow + i - itemsPerPage*currentPage, 2 + padding, displayText, color);
            }

            // Check state to show arrows or not
            if (currentPage < maxPage)
            {
                AddText(18, 56, "->");
            } else
            {
                AddText(18, 56, "  ");
            }
            if (currentPage != 0)
            {
                AddText(18, 2, "<-");
            } else
            {
                AddText(18, 2, "  ");
            }

            Render();
            
            // Reset the input queue so multiple old events dont get run
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            // Get user input
            ConsoleKey Key = Console.ReadKey(true).Key;

            // Update selection based on arrow keys
            switch (Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedIndex < Opgaver.Count - 1)
                    {
                        selectedIndex++;
                    }
                    break;
                case ConsoleKey.Enter:
                    // Handle the selection
                    string selectedOpgave = Opgaver[selectedIndex];

                    MethodInfo method = typeof(Program).GetMethod(selectedOpgave, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    if (method != null)
                    {
                        Console.Clear();
                        Console.CursorVisible = true;
                        method.Invoke(null, null);
                        Console.WriteLine("");
                        Console.WriteLine("Tryk på en knap for at gå videre...");
                        Console.ReadKey();
                        Console.Clear();
                        Render(true);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    // Check if another page could exist?
                    if (currentPage < maxPage)
                    {
                        currentPage++;
                        selectedIndex = itemsPerPage * currentPage;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentPage != 0)
                    {
                        currentPage--;
                        selectedIndex = itemsPerPage * currentPage;
                    }
                    break;
            }
        } while (on);
    }
}
