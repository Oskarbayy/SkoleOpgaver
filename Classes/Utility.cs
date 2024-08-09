using System.Diagnostics;

public class Utility
{
    // Unicode characters for box drawing
    private const string Horizontal = "\u2500";
    private const string Vertical = "\u2502";
    private const string TopRight = "\u2510";
    private const string TopLeft = "\u250C";
    private const string BottomRight = "\u2518";
    private const string BottomLeft = "\u2514";

    private const string DownwardT = "\u252C";
    private const string UpwardT = "\u2534";
    private const string RightwardT = "\u251C";
    private const string LeftwardT = "\u2524";
    private const string Cross = "\u253C";

    public string GetIntersectionCharacter(string[,] buffer, int x, int y, int width, int height, string currentChar)
    {
        bool up = y > 0 && (buffer[y - 1, x] == Vertical || buffer[y - 1, x] == DownwardT || buffer[y - 1, x] == UpwardT || buffer[y - 1, x] == Cross);
        bool down = y < height - 1 && (buffer[y + 1, x] == Vertical || buffer[y + 1, x] == DownwardT || buffer[y + 1, x] == UpwardT || buffer[y + 1, x] == Cross);
        bool left = x > 0 && (buffer[y, x - 1] == Horizontal || buffer[y, x - 1] == LeftwardT || buffer[y, x - 1] == RightwardT || buffer[y, x - 1] == Cross);
        bool right = x < width - 1 && (buffer[y, x + 1] == Horizontal || buffer[y, x + 1] == LeftwardT || buffer[y, x + 1] == RightwardT || buffer[y, x + 1] == Cross);

        if (up && down && left && right) return Cross; // cross
        if (up && down && left) return LeftwardT; // leftward T
        if (up && down && right) return RightwardT; // rightward T
        if (up && left && right) return UpwardT; // upward T
        if (down && left && right) return DownwardT; // downward T

        // If it's a corner without a direct T or cross connection
        if (up && right) return BottomLeft;
        if (up && left) return BottomRight;
        if (down && right) return TopLeft;
        if (down && left) return TopRight;

        return currentChar; // return the original character if no connection is found
    }

    public void DrawBox(Buffer buffer, int row, int col, int height, int width)
    {
        // Draw top and bottom
        for (int curCol = col; curCol < col + width; curCol++)
        {
            buffer.Set(row, curCol, Horizontal); // Top
            buffer.Set(row + height - 1, curCol, Horizontal); // Bottom 
        }

        // Draw walls left and right
        for (int curRow = row; curRow < row + height; curRow++)
        {
            buffer.Set(curRow, col, Vertical); // Left
            buffer.Set(curRow, col + width - 1, Vertical); // Right
        }

        // Set corners
        buffer.Set(row, col, TopLeft);
        buffer.Set(row, col + width - 1, TopRight);
        buffer.Set(row + height - 1, col, BottomLeft);
        buffer.Set(row + height - 1, col + width - 1, BottomRight);
    }

    public void AddText(Buffer buffer, int row, int col, string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        Debug.WriteLine(text);
        Debug.WriteLine("Foreground: "+foregroundColor);
        Debug.WriteLine("Backgrond: " + backgroundColor);

        for (int i = 0; i < text.Length; i++)
        {
            buffer.Set(row, col + i, text[i].ToString(), foregroundColor, backgroundColor);
        }
    }
}
