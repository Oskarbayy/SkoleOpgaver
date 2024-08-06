
partial class Program
{
    static void IgangMedLøkker()
    {
        Console.WriteLine("For loop fra 0-99");
        for (int i1 = 0; i1 < 100; i1++)
        {

            Console.WriteLine(i1);
        }
        Console.WriteLine();

        Console.WriteLine("For loop fra 0-99 med if statement");
        for (int i1 = 0; i1 < 100; i1++)
        {
            if (i1 < 50)
            {
                Console.WriteLine(i1);
            }
        }
        Console.WriteLine();

        Console.WriteLine("While loop fra 0-99 med if statement");
        int i2 = -1;
        while (i2++ < 100)
        {
            if (i2 < 50)
            {
                Console.WriteLine(i2);
            }
        }
        Console.WriteLine();

        Console.WriteLine("For loop fra 100-0");
        for (int i3 = 100; i3 >= 0; i3--)
        {
            Console.WriteLine(i3);
        }
    }
}
