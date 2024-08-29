using DigitalClock.Common;
using System.Diagnostics;
using System.Drawing;

namespace DigitalClock.Models
{
    internal class Clock
    {
        // SETTINGS
        public int SegmentSize = 2;

        private int _xOffset;
        private int _yOffset;
        private bool _running = true;

        private int _usableRows;
        private string[] _oldClockLines;
        private int _xClockWidth;
        private bool _ColonLastShowed = false;
        private ConsoleColor _color = ConsoleColor.White;

        public Clock()
        {

            _usableRows = SegmentSize * Constants.VerticalSegments + Constants.HorizontalSegments;
            _oldClockLines = new string[_usableRows];

            _xClockWidth = (SegmentSize + 2) * Constants.Digits + Constants.Spaces; // if it works it works

            _xOffset = Console.WindowWidth / 2 - _xClockWidth / 2;
            _yOffset = Console.WindowHeight / 2 - _usableRows / 2;
        }

        public void Start()
        {
            _running = true;
            Debug.WriteLine(_xOffset + " " + _yOffset);
            Utility.DrawBox((_xOffset - 1, _yOffset - 1), (_xOffset + _xClockWidth + 1, _yOffset + _usableRows));
            Utility.DrawControls();


            while (_running)
            {
                string now = DateTime.Now.ToLongTimeString();
                DrawTime(now);
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _running = false;
        }

        public void ChangeColor(bool add)
        {
            _color += (add ? 1 : -1);

            if ((int)_color > 15)
            {
                _color = (ConsoleColor)1;
            }
            else if ((int)_color < 1)
            {
                _color = (ConsoleColor)15;
            }

            Debug.WriteLine(_color);
            DrawTime(DateTime.Now.ToLongTimeString(), true);
        }

        public bool On()
        {
            return _running;
        }

        public void DrawTime(string time, bool DrawAll = false)
        {
            Console.ForegroundColor = (ConsoleColor)_color;

            // Draw all is an option to quickly load the clock again but with new colors
            if (DrawAll)
            {
                // Clear the console but the last line
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < Console.WindowHeight - 1; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                //

                Utility.DrawBox((_xOffset - 1, _yOffset - 1), (_xOffset + _xClockWidth + 1, _yOffset + _usableRows));
            }

            // create a string array that needs to be built for the output
            string[] ClockLines = new string[_usableRows];

            // Loop through each row
            for (int row = 0; row < ClockLines.Length; row++)
            {
                string line = "";

                // For each row we loop through all the chars / digits
                foreach (char c in time)
                {
                    // If its not a digit but a colon then we need to account for thatt
                    if (c == ':')
                    {
                        if (_ColonLastShowed)
                        {
                            line += (row == SegmentSize || row == SegmentSize + 2) ? " " : " ";
                        }
                        else
                        {
                            line += (row == SegmentSize || row == SegmentSize + 2) ? "o" : " ";
                        }
                        continue;
                    }

                    // if its a digit then we need to use the segments from the byte array
                    int digit = int.Parse(c.ToString());
                    line += GetSegment(row, Constants.DigitPatterns[digit]);

                    // Make spacing in between every digit
                    line += " ";
                }


                // Line has now been built
                ClockLines[row] = line.TrimEnd();

                // If line has changed since last iteration then we write that line again
                if (DrawAll)
                {
                    Console.SetCursorPosition(_xOffset, row + _yOffset);
                    Console.Write(ClockLines[row] + new string(' ', _xClockWidth - ClockLines[row].Length));
                }
                else if (ClockLines[row] != _oldClockLines[row])
                {
                    Console.SetCursorPosition(_xOffset, row + _yOffset);
                    Console.Write(ClockLines[row] + new string(' ', _xClockWidth - ClockLines[row].Length));
                }
            }

            _oldClockLines = ClockLines;
            if (!DrawAll)
            {
                _ColonLastShowed = !_ColonLastShowed;
            }
        }

        public string GetSegment(int row, byte digitPattern)
        {
            string horizontal = new string('─', SegmentSize);
            string vertical = "│";
            string empty = new string(' ', SegmentSize);

            // Horizontal rows:
            if (row == 0)
            { // If we are on the top row of the clock then we need to check if this digit has that segment
                return (digitPattern & (1 << 0)) != 0 ? $" {horizontal} " : $" {empty} ";
            }
            else if (row == SegmentSize + 1)
            { // Middle Horizontal Row
                return (digitPattern & (1 << 6)) != 0 ? $" {horizontal} " : $" {empty} ";
            }
            else if (row == SegmentSize * 2 + 2)
            { // Bottom Horizontal Row
                return (digitPattern & (1 << 3)) != 0 ? $" {horizontal} " : $" {empty} ";
            }
            else if (row > 0 & row < SegmentSize + 1)
            { // Top rows used for the vertical lines
                string left = (digitPattern & (1 << 5)) != 0 ? vertical : " ";
                string right = (digitPattern & (1 << 1)) != 0 ? vertical : " ";
                return $"{left}{empty}{right}";
            }
            else if (row > SegmentSize + 1 & row < SegmentSize * 2 + 2)
            { // Bottom rows used for the vertical lines
                string left = (digitPattern & (1 << 4)) != 0 ? vertical : " ";
                string right = (digitPattern & (1 << 2)) != 0 ? vertical : " ";
                return $"{left}{empty}{right}";
            }

            // Callback?
            return empty;
        }
    }
}
