using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalClock.Common
{
    static class Utility
    {
        public static void DrawBox((int x, int y) topLeft, (int x, int y) bottomRight)
        {
            Console.SetCursorPosition(topLeft.x, topLeft.y);

            int width = bottomRight.x - topLeft.x + 1;
            int height = bottomRight.y - topLeft.y + 1;

            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(topLeft.x, topLeft.y+y);
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 && x == 0)
                        Console.Write("┌");
                    else if (y == 0 && x == width - 1)
                        Console.Write("┐");
                    else if (y == height - 1 && x == 0)
                        Console.Write("└");
                    else if (y == height - 1 && x == width - 1)
                        Console.Write("┘");
                    else if (y == 0 || y == height - 1)
                        Console.Write("─");
                    else if (x == 0 || x == width - 1)
                        Console.Write("│");
                    else
                        Console.Write(" ");
                }

            }
        }

        private static string[] _controls =
        {
            "[LeftArrow] Prev Color",
            "[RightArrow] Next Color"
        };
        public static void DrawControls()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _controls.Length; i++)
            {
                stringBuilder.Append(_controls[i]);
                if (i+1 < _controls.Length)
                {
                    stringBuilder.Append(", ");
                }
            }

            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            string Controls = stringBuilder.ToString();


            Console.SetCursorPosition(Console.WindowWidth / 2-Controls.Length/2, Console.WindowHeight-1);
            Console.Write(Controls);

            Console.ForegroundColor = prevColor;
        }
    }
}
