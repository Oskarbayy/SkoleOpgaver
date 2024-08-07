using System;

public class Buffer : Utility
{
    private static int rows = 20;
    private static int cols = 60;

    private static (string character, ConsoleColor color)[,] _buffer;
    private static (string character, ConsoleColor color)[,] _previousBuffer;

    static Buffer()
    {
        _buffer = new (string, ConsoleColor)[rows, cols];
        _previousBuffer = new (string, ConsoleColor)[rows, cols];
        Clear();
    }

    public static void Clear()
    {
        for (int row = 0; row < _buffer.GetLength(0); row++)
        {
            for (int col = 0; col < _buffer.GetLength(1); col++)
            {
                _buffer[row, col] = (" ", ConsoleColor.Black);
                _previousBuffer[row, col] = ("", ConsoleColor.Black);
            }
        }
    }

    public static void Set(int row, int col, string value, ConsoleColor color = ConsoleColor.Black)
    {
        // Ensure position is within the 'screen' / buffer bounds
        if (row >= 0 && row < _buffer.GetLength(0) && col >= 0 && col < _buffer.GetLength(1))
        {
            _buffer[row, col] = (value, color);
        }
    }

    public static void Render(bool priority = false)
    {
        for (int row = 0; row < _buffer.GetLength(0); row++)
        {
            for (int col = 0; col < _buffer.GetLength(1); col++)
            {
                var (character, color) = _buffer[row, col];
                var (prevCharacter, prevColor) = _previousBuffer[row, col];

                // Only update if there's a change
                if (priority || character != prevCharacter || color != prevColor)
                {
                    Console.SetCursorPosition(col, row);
                    if (color == ConsoleColor.White)
                    {
                        Console.BackgroundColor = color;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(character);
                    Console.ResetColor();
                }

                // Update the previous buffer
                _previousBuffer[row, col] = (character, color);
            }
        }
        Console.SetCursorPosition(0, _buffer.GetLength(0)); // Move cursor out of the way
        Console.ResetColor();
    }
}
