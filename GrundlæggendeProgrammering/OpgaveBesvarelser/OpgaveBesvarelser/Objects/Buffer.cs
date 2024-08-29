using System;
using System.Drawing;

public class Buffer : Utility
{
    private int rows;
    private int cols;

    private (string character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)[,] _buffer;
    private (string character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)[,] _previousBuffer;

    public Buffer(int rows = 20, int cols = 60)
    {
        this.rows = rows;
        this.cols = cols;
        _buffer = new (string, ConsoleColor, ConsoleColor)[rows, cols];
        _previousBuffer = new (string, ConsoleColor, ConsoleColor)[rows, cols];
        Clear();
    }

    public void Clear()
    {
        for (int row = 0; row < _buffer.GetLength(0); row++)
        {
            for (int col = 0; col < _buffer.GetLength(1); col++)
            {
                _buffer[row, col] = (" ", ConsoleColor.White, ConsoleColor.Black);
                _previousBuffer[row, col] = ("", ConsoleColor.White, ConsoleColor.Black);
            }
        }
    }

    public void Set(int row, int col, string value, ConsoleColor foregroundColor = ConsoleColor.White, 
        ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        // Ensure position is within the 'screen' / buffer bounds
        if (row >= 0 && row < _buffer.GetLength(0) && col >= 0 && col < _buffer.GetLength(1))
        {
            _buffer[row, col] = (value, foregroundColor, backgroundColor);
        }
    }

    public void Render(bool priority = false)
    {
        Console.CursorVisible = false;

        // Gem seneste brugt colors for at bruge memory istedet for performance på at rendere hurtigere
        ConsoleColor lastForegroundColor = Console.ForegroundColor;
        ConsoleColor lastBackgroundColor = Console.BackgroundColor;

        for (int row = 0; row < _buffer.GetLength(0); row++)
        {
            for (int col = 0; col < _buffer.GetLength(1); col++)
            {
                var (character, foregroundColor, backgroundColor) = _buffer[row, col];
                var (prevCharacter, prevFore, prevBack) = _previousBuffer[row, col];

                // Only update if there's a change
                if (priority || character != prevCharacter || foregroundColor != prevFore || backgroundColor != prevBack)
                {
                    // Set cursor position
                    Console.SetCursorPosition(col, row);

                    // Use old colors to check if Console.BackgroundColor is needed since its slow and not fun
                    if (backgroundColor != lastBackgroundColor)
                    {
                        Console.BackgroundColor = backgroundColor;
                        lastBackgroundColor = backgroundColor;
                    }

                    if (foregroundColor != lastForegroundColor)
                    {
                        Console.ForegroundColor = foregroundColor;
                        lastForegroundColor = foregroundColor;
                    }

                    Console.Write(character);
                }

                // Update the previous buffer
                _previousBuffer[row, col] = (character, foregroundColor, backgroundColor);
            }
        }
        Console.SetCursorPosition(0, _buffer.GetLength(0)); // Move cursor out of the way
        Console.ResetColor();
    }
}
