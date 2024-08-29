
partial class Program
{

    static void ArrayOgBubblesort()
    {
        try
        {
            // Opret arrayet
            int[] array = new int[100];


            for (int i = 0; i < array.Length; i++) 
            {
                array[i] = random.Next(1, 100);
            }

            Console.WriteLine("Random Array:");
            foreach (int i in array)
            {
                Console.Write(i+" ");
            }


            // Calculate Sorted Array
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        // if number is bigger then move it up the array and the less number down the array (switching index)
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nSorteret Array:");
            foreach (int i in array)
            {
                Console.Write(i + " ");
            }
            
            // Reverse the array
            Array.Reverse(array);

            // Print the reversed array
            Console.WriteLine("\n\nReversed Array:");
            foreach (int i in array)
            {
                Console.Write(i+" ");
            }

            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
