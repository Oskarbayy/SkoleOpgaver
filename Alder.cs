partial class Program
{
    static void Alder()
    {
        try
        {
            // Prompt
            Console.Write("Navn: ");
            string navn = Console.ReadLine();

            Console.Write("Alder: ");
            double alder = double.Parse(Console.ReadLine());

            Console.WriteLine();

            if (alder < 3)
            {
                Console.WriteLine(navn + " du må gå med ble");
            } else if (alder < 15)
            {
                Console.WriteLine(navn + ", du må ingenting");
            } else if (alder < 18)
            {
                Console.WriteLine(navn + ", du må drikke");
            } else
            {
                Console.WriteLine(navn + ", du må stemme og køre bil");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            Alder();
        }
    }
}
