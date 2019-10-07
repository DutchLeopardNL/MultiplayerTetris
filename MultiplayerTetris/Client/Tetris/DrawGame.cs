using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Tetris
{
    class Tetrominoe
    {
        public static void Draw()
        {
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(1 + 2 * j, i);

                }

            }
        }
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
                Console.Write("*-");
            }
        }
    }
}
