using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Tetris
{
    class DrawGame
    {
        public static int linesCleared = 0, score = 0, level = 1;
        public static void DrawBorder()
        {
            for (int lengthCount = 0; lengthCount <= 22; lengthCount++)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("|");
                Console.SetCursorPosition(21, lengthCount);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 23);
            for (int widthCount = 0; widthCount <= 10; widthCount++)
            {
                Console.Write("**");
            }
            Console.SetCursorPosition(25, 0);
            Console.WriteLine($"Level: {level} ");
            Console.SetCursorPosition(25, 1);
            Console.WriteLine($"Score: {score} ");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine($"LinesCleared: {linesCleared}");
        }
    }
}
