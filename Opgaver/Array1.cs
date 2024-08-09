partial class Program
{
    static void Array1()
    {
        int[] array = new int[9];

        for (int i = 0; i < 9; i++)
        {
            array[i] = i + 1;
        }
        Console.WriteLine("Index 5 som skal fordoblets i arrayet er: " + array[5]);
        array[5] = array[5] * 2;

        Console.WriteLine();
        Console.WriteLine("Result Array:");
        foreach (int i in array)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
