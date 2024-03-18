using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Town
{
    enum MapSize
    {
        Height = 15,
        Width = 10
    }
    /*
    0 = ■
    1 = │
    2 = ─
    3 = └
    4 = ┘
    5 = ┌
    6 = ┐
    7 = start Point

    if 10 > n, n = □

    11 = dungeon, 12 = Inn, 13 = Shop
     */
    class Map
    {
        int[,] arr =
        {

            { 0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,3,2,2,11,11,2,2,4,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 2,2,2,6,0,0,0,0,0,0,0,0,0,0,0,0,5,2,2,2 },
            { 0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0 },
            { 0,0,0,13,0,0,0,0,0,0,0,0,0,0,0,0,12,0,0,0 },
            { 0,0,0,13,0,0,0,0,0,0,0,0,0,0,0,0,12,0,0,0 },
            { 0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0 },
            { 2,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,3,2,2,2 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,0 },

        };
        public void MapDrawer(int[,] arr)
        {
            for(int i = 0; i < (int)MapSize.Height; i++)
            {
                for (int k = 0; k < (int)MapSize.Width; k++)
                {
                    if (arr[i, k] == 0)
                        Console.WriteLine("■");
                    if (arr[i, k] == 1)
                        Console.WriteLine("│");
                    if (arr[i, k] == 2)
                        Console.WriteLine("─");
                    if (arr[i, k] == 3)
                        Console.WriteLine("└");
                    if (arr[i, k] == 4)
                        Console.WriteLine("┘");
                    if (arr[i, k] == 5)
                        Console.WriteLine("┌");
                    if (arr[i, k] == 6)
                        Console.WriteLine("┐");
                    if (arr[i, k] > 10)
                        Console.WriteLine("□");
                    if (arr[i, k] == 7)
                        Console.WriteLine("θ");
                }
            }
        }
        public void InputKey()
        {
            ConsoleKeyInfo input = Console.ReadKey();
        }
    }
}
