/*
-	Lav et array der indeholder 7 tilfældige lottoTal mellem 1 og 20. Dette er vinder-kuponen. 

-	Lav nu endnu et array med 7 tal der repræsenterer en brugers kupon. 

-	I kan lade brugeren indtaste de 7 tal, eller i kan ”hardcode” brugerens kupon. 

-	Undersøg hvor mange rigtige tal der er på brugerens kupon, og udbetal forskellige gevinster på kuponer der har mere end to rigtige.
*/


partial class Program
{
    static int lottoBaseReward = 10; // kr?
    static void Lotto()
    {
        try
        {
            // Generate winner lotto array with 7 random numbers [1-20] (random is already defined in this class)
            int[] winnerLotto = new int[7];
            for (int i = 0; i < 7; i++)
            {
                winnerLotto[i] = random.Next(1, 21);
            }

            Console.WriteLine("Dine lotto numre er:");

            // Generate user lotto array
            int[] userLotto = new int[7];
            for (int i = 0; i < 7; i++)
            {
                userLotto[i] = random.Next(1, 21);
                Console.Write(userLotto[i] +" ");
            }

            // Check Result
            int corrects = 0;
            foreach (int i in winnerLotto)
            {
                if (userLotto.Contains(i))
                {
                    corrects++;
                }

            }

            if (corrects > 2)
            {
                Console.WriteLine("\n\nDu havde " + corrects + " rigtige tal og vandt " + Math.Pow(lottoBaseReward, corrects) + " kr!!");

            } else
            {
                Console.WriteLine("\n\nDu havde " + corrects + " rigtig tal og vandt ikke noget...");

            }
            //

            Console.WriteLine("\nDe rigtige numre var:");

            // Reveal Winner Numbers
            foreach (int i in winnerLotto)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fejl, Prøv Igen");
            Lotto();
        }
        Console.WriteLine("\nTryk på en knap for at forsætte...");
        Console.ReadKey();
    }
}
