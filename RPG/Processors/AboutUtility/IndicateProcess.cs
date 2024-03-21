using Entities;
using Managers;
using Equipments;

namespace Processors
{
    

    class IndicateProcess
    {
        Player player;

        public IndicateProcess(Player player)
        {
            this.player = player;
        }
        public static void Indicator(Player player, Monster mon)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌──────────────────────────────────────────────────┐");
            Console.Write($"│{player.Name}'s HP : {player.CurHP}\t\t\t\t   │");
            if (player.CurHP < 100) Console.Write(" \n"); else Console.Write('\n');
            Console.WriteLine($"│{player.Name}'s MP : {player.CurMP}\t\t{mon.Name}'s HP : {mon.CurHP}   │");
            Console.WriteLine($"│{player.Name}'s Atk : {player.Atk}     VS \t{mon.Name}'s Atk : {mon.Atk}   │");
            Console.WriteLine($"│{player.Name}'s Def : {player.Def}   \t\t{mon.Name}'s Def : {mon.Def}   │");
            Console.WriteLine($"│{player.Name}'s EXP : {player.curEXP}/{player.maxEXP}\t\t\t\t   │");
            Console.WriteLine($"│{player.Name}'s LV : {player.LV}                                   │");
            Console.WriteLine("└──────────────────────────────────────────────────┘\n");
            Console.ResetColor();
        }
        public void Status()
        {
            Cleaner.CleanBox();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n┌───────────────────────────────┐");
            Console.Write($"│\t{player.Name}'s HP : {player.CurHP}\t│");
            if (player.CurHP < 100) Console.Write(" \n"); else Console.Write('\n');
            Console.WriteLine($"│\t{player.Name}'s MP : {player.CurMP}\t│");
            Console.WriteLine($"│\t{player.Name}'s Atk : {player.Atk}\t│");
            Console.WriteLine($"│\t{player.Name}'s Def : {player.Def}\t│");
            Console.WriteLine($"│\t{player.Name}'s EXP : {player.curEXP}/{player.maxEXP}\t│");
            Console.WriteLine($"│\t{player.Name}'s LV : {player.LV}\t\t│");
            Console.WriteLine($"│{player.Name}'s Gold : {player.Gold}Gold\t│");
            Console.WriteLine("└───────────────────────────────┘");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();

        }
    }
}
