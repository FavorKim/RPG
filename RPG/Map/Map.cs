using Entities;
using Managers;
using Processors;
using Select;
using Equipments;
using Selectable;

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
        Status,
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

            int num = InputProcessor.Input(2);
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
        ItemManager iM;
        EquipManager eM;
        IndicateProcess iP;
        MenuSelector iStat;
        public int[,] arr =
        {

            { 0,0,0,0,0,0,1,0,31,32,33,0,0,1,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,1,0,34,35,36,37,0,1,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,3,2,2,2,11,11,11,11,2,2,2,4,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 2,2,2,6,0,0,0,0,0,0,0,0,0,0,0,0,5,2,2,2 },
            { 51,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,41,0,0 },
            { 52,0,13,0,0,0,0,0,0,0,0,0,0,0,0,12,0,42,0,0 },
            { 53,0,13,0,0,0,0,0,0,0,0,0,0,0,0,12,0,42,0,0 },
            { 54,0,1,0,0,0,0,0,0,0,0,0,0,0,0,12,0,0,0,0 },
            { 2,2,2,4,0,0,0,0,0,0,0,0,0,0,0,0,3,2,2,2 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },

        };
        void DrawDungeon(int num)
        {
            if (num == 31) Console.Write(" D");
            if (num == 32) Console.Write("U");
            if (num == 33) Console.Write("N");
            if (num == 34) Console.Write("G");
            if (num == 35) Console.Write("E");
            if (num == 36) Console.Write("O");
            if (num == 37) Console.Write("N");
        }
        void DrawInn(int num)
        {
            if (num == 41) Console.Write("I");
            if (num == 42) Console.Write("N");
        }
        void DrawShop(int num)
        {
            if (num == 51) Console.Write("S");
            if (num == 52) Console.Write("H");
            if (num == 53) Console.Write("O");
            if (num == 54) Console.Write("P");

        }
        int[,] buffer;
        int player = 7;
        int pX = 0;
        int pY = 13;

        public Map(ItemManager iM, EquipManager eM, IndicateProcess iP, MenuSelector iStat)
        {
            MBuffer = new Buffer<int>(20, 15, MapDrawer, arr);
            buffer = MBuffer.GetArray2();
            arr[pY, pX] = player;
            this.iM = iM;
            this.eM = eM;
            this.iP = iP;
            this.iStat = iStat;
        }

        public void MapDrawer()
        {
            for (int i = 0; i < (int)MapSize.Height; i++)
            {
                for (int k = 0; k < (int)MapSize.Width; k++)
                {
                    if (buffer[i, k] < 0)
                        continue;
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
                    if (buffer[i, k] > 10 )
                        Console.Write(" ");
                    if (buffer[i, k] == 7 && buffer[i, k] != 0)
                        Console.Write("◈");
                    DrawDungeon(buffer[i, k]);
                    DrawInn(buffer[i, k]);
                    DrawShop(buffer[i, k]);
                }
                Console.WriteLine();
            }

        }

        public Entering Move()
        {

            Entering en = Entering.Fail;
            arr[pY, pX] = 0;

            ConsoleKeyInfo input;

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

                case ConsoleKey.Escape:
                    TextBox temp = (TextBox)iStat.selP.SelectReturn();
                    
                    if(temp != null)
                    {
                        temp.Use();
                        Cleaner.CleanBox();
                        break;
                    }
                    Cleaner.CleanBox();

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

