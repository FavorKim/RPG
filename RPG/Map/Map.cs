using Entities;
using Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public enum MapSize
    {
        Height = 14,
        Width = 20
    }
    public enum Entering
    {
        Shop,
        Dungeon,
        Inn,
        Fail,
    }

    class Inn
    {
        Player player;
        public Inn(Player player) { this.player = player; }

        public void Welcome()
        {
            Console.Clear();
            Console.WriteLine("100Gold for Rest. \n Do you want to Rest?\n");
            Console.WriteLine($"1. Yes, 2. NoThanks\n You Have {player.Gold}Gold");
            Select();
            Console.Clear();
        }

        public void Rest()
        {
            if (player.Gold >= 100)
            {
                Console.WriteLine("Fully Recovered!");
                player.Gold -= 100;
                player.FullRecover();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not Enough Money.");
                Console.ReadLine();
            }
            Console.Clear();

        }

        public void Select()
        {

            int num = InputProcess.Input(2);
            if (num == 1)
                Rest();
            else
                return;
            Console.Clear();

        }
    }

    class Map
    {
        public Buffer<int> MBuffer;
        public int[,] arr =
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
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },

        };
        int[,] buffer; 
        int player = 7;
        int pX = 0;
        int pY = 13;
        ShopProcessor sP;
        Inn inn;

        public Map(ShopProcessor sP, Inn inn)
        {
            MBuffer = new Buffer<int>(20 , 15, MapDrawer, arr);
            buffer = MBuffer.GetArray2();
            arr[pY, pX] = player;
            this.sP = sP;
            this.inn = inn;
        }

        public void MapDrawer()
        {
            for (int i = 0; i < (int)MapSize.Height; i++)
            {
                for (int k = 0; k < (int)MapSize.Width; k++)
                {
                    if (buffer[i, k] == 0)
                        Console.Write(" ");
                    if (buffer[i, k] == 1)
                        Console.Write("│");
                    if (buffer[i, k] == 2)
                        Console.Write("─");
                    if (buffer[i, k] == 3)
                        Console.Write("└");
                    if (buffer[i, k] == 4)
                        Console.Write("┘");
                    if (buffer[i, k] == 5)
                        Console.Write("┌");
                    if (buffer[i, k] == 6)
                        Console.Write("┐");
                    if (buffer[i, k] > 10)
                        Console.Write(" ");
                    if (buffer[i, k] == 7 && buffer[i, k] != 0)
                        Console.Write("◈");
                }
                Console.WriteLine();
            }
            
        }

        public Entering Move()
        {

            ConsoleKeyInfo input;
            Entering en=Entering.Fail;
            arr[pY, pX] = 0;

            input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (pX <= 0)
                        break;
                    if (arr[pY, pX - 1] == 0)
                        pX -= 1;
                    else if (arr[pY, pX - 1] > 10)
                    {
                        en = Enter(arr[pY, pX - 1]);
                        Console.Clear();
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (pX % ((int)MapSize.Width - 1) == 0 && pX != 0)
                        break;
                    if (arr[pY, pX + 1] == 0)
                        pX += 1;
                    else if (arr[pY, pX + 1] > 10)
                    {
                        en = Enter(arr[pY, pX + 1]); 
                        Console.Clear();
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (pY == 0)
                        break;
                    if (arr[pY - 1, pX] == 0)
                        pY -= 1;
                    else if (arr[pY - 1, pX] > 10)
                    {
                        en = Enter(arr[pY - 1, pX]);
                        Console.Clear();
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (pY >= (int)MapSize.Height - 1)
                        break;
                    if (arr[pY + 1, pX] == 0)
                        pY += 1;
                    else if (arr[pY + 1, pX] > 10)
                    {
                        en = Enter(arr[pY + 1, pX]);
                        Console.Clear();
                    }
                    break;
                default:
                    en = Entering.Fail;
                    break;
            }

            arr[pY, pX] = player;
            return en;
        }

        public Entering Enter(int pos)
        {
            if (pos == 12)
            {
                Console.Clear();
                return Entering.Inn;
            }

            if (pos == 13)
            {
                Console.Clear();
                return Entering.Shop;
            }

            if (pos == 11)
            {
                Console.Clear();
                return Entering.Dungeon;
            }
            else 
                return Entering.Fail;
        }

    }

    class Buffer<T>
    {
        T[] buffer;
        T[,] buffer2;
        T[] org;
        T[,] org2;

        Action print;
        int width;
        int height;

        public Buffer(int size, Action print, T[] org)
        {
            buffer = new T[size];
            this.print = print;
            this.org = org;
            DrawBuffer(org);
        }
        public Buffer(int width, int height, Action print, T[,] org2)
        {
            buffer2 = new T[height,width];
            this.width = width;
            this.height = height;
            this.print = print;
            this.org2 = org2;
            DrawBuffer(org2);
        }

        public T[] GetArray() { return buffer; }
        public T[,] GetArray2() { return buffer2; }
        

        void DrawBuffer(T[] org) { org.CopyTo(buffer, 0);}
        void DrawBuffer(T[,] org) 
        {
            for(int i=0; i < height; i++)
            {
                for(int k=0; k<width; k++)
                {
                    buffer2[i, k] = org[i, k];
                }
            }
        }

        public void Show()
        {
            DrawBuffer(org);
            Console.SetCursorPosition(0, 0);
            print();
        }
        public void Show2()
        {
            DrawBuffer(org2);
            Console.SetCursorPosition(0, 0);
            print();
        }



    }
    

    
    
}
/*
0 = ■
1 = │
2 = ─
3 = └
4 = ┘
5 = ┌
6 = ┐
7 = Character
if 10 > n, n = □
*/

/*
11 = dungeon, 12 = Inn, 13 = Shop
 */

