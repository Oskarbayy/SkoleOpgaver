
using System.Text;

partial class Program
{
    private static readonly Dictionary<char, string> morseCodeDictionary = new Dictionary<char, string>
    {
        {'A', ".-"},
        {'B', "-..."},
        {'C', "-.-."},
        {'D', "-.."},
        {'E', "."},
        {'F', "..-."},
        {'G', "--."},
        {'H', "...."},
        {'I', ".."},
        {'J', ".---"},
        {'K', "-.-"},
        {'L', ".-.."},
        {'M', "--"},
        {'N', "-."},
        {'O', "---"},
        {'P', ".--."},
        {'Q', "--.-"},
        {'R', ".-."},
        {'S', "..."},
        {'T', "-"},
        {'U', "..-"},
        {'V', "...-"},
        {'W', ".--"},
        {'X', "-..-"},
        {'Y', "-.--"},
        {'Z', "--.."},
        {'Æ', ".-.-"},
        {'Ø', "---."},
        {'Å', ".--.-"}
    };

    static void MorseKoden()
    {
        try
        {
            // Prompt
            Console.Write("Skriv noget og jeg vil translate det til morsekode: ");
            string Text = Console.ReadLine().ToLower();
            
            StringBuilder translatedMorseCode = new StringBuilder();

            foreach (char character in Text.ToUpper())
            {
                if (morseCodeDictionary.ContainsKey(character))
                {
                    translatedMorseCode.Append(morseCodeDictionary[character] + " ");
                }
            }

            // Result
            Console.Write("Morse kode: ");
            Console.WriteLine(translatedMorseCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            MorseKoden();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
