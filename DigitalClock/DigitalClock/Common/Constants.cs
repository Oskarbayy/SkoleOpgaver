using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalClock.Common
{
    static class Constants
    {
        public static readonly byte[] DigitPatterns = new byte[]
        {
            0b00111111, // 0 a b c d e f 
            0b00000110, // 1 b c
            0b01011011, // 2 a b d e g
            0b01001111, // 3 a b c d g
            0b01100110, // 4 b c f g
            0b01101101, // 5 a c d f g
            0b01111100, // 6 c d e f g
            0b00000111, // 7 a b c
            0b01111111, // 8 a b c d e f g
            0b01100111, // 9 a b c f g
        };


        //
        public static readonly int RowsForDateDisplay = 5;
        public static readonly int VerticalSegments = 2;
        public static readonly int HorizontalSegments = 3;
        public static readonly int Digits = 6;

        // This only works if i dont scale the spaces between digits but no plan for doing that so far
        public static readonly int Spaces = 7;


    }
}
