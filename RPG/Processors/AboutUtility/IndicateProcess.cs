using Entities;
using Managers;
using Equipments;
using System.ComponentModel.Design;

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
            Console.WriteLine("┌───────────────────────────────────────────────────────┐");
            if (player.CurHP >= 100 && player.MaxHP >= 100)
                Console.WriteLine($"│{player.Name}'s HP\t: {player.CurHP}/{player.MaxHP}\t\t\t│");
            else
                Console.WriteLine($"│{player.Name}'s HP\t: {player.CurHP}/{player.MaxHP}\t\t\t\t\t│");
            Console.WriteLine($"│{player.Name}'s MP\t: {player.CurMP}/{player.MaxMP}\t\t{mon.Name}'s HP\t: {mon.CurHP}\t│");
            Console.WriteLine($"│{player.Name}'s Atk\t: {player.Atk}\tVS\t{mon.Name}'s Atk\t: {mon.Atk}\t│");
            Console.WriteLine($"│{player.Name}'s Def\t: {player.Def}\t\t{mon.Name}'s Def\t: {mon.Def}\t│");
            if (player.curEXP >= 100 || player.maxEXP >= 100)
                Console.WriteLine($"│{player.Name}'s EXP\t: {player.curEXP}/{player.maxEXP}\t\t\t\t│");
            else
                Console.WriteLine($"│{player.Name}'s EXP\t: {player.curEXP}/{player.maxEXP}\t\t\t\t\t│");
            Console.WriteLine($"│{player.Name}'s LV\t: {player.LV}\t\t\t\t\t│");
            Console.WriteLine("└───────────────────────────────────────────────────────┘\n");
            Console.ResetColor();
        }
        public void Status()
        {
            Cleaner.CleanBox();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n┌───────────────────────────────────────┐");
            Console.Write($"│\t{player.Name}'s HP\t: {player.CurHP}/{player.MaxHP}\t\t│");
            if (player.CurHP < 100) Console.Write(" \n"); else Console.Write('\n');
            Console.WriteLine($"│\t{player.Name}'s MP\t: {player.CurMP}/{player.MaxMP}\t\t│");
            Console.WriteLine($"│\t{player.Name}'s Atk\t: {player.Atk}\t\t│");
            Console.WriteLine($"│\t{player.Name}'s Def\t: {player.Def}\t\t│");
            if (player.curEXP >= 100 || player.maxEXP >= 100)
                Console.WriteLine($"│\t{player.Name}'s EXP\t: {player.curEXP}/{player.maxEXP}\t│");
            else
                Console.WriteLine($"│\t{player.Name}'s EXP\t: {player.curEXP}/{player.maxEXP}\t\t│");
            Console.WriteLine($"│\t{player.Name}'s LV\t: {player.LV}\t\t│");
            if (player.Gold >= 10)
                Console.WriteLine($"│\t{player.Name}'s Gold\t: {player.Gold}Gold\t│");
            else
                Console.WriteLine($"│\t{player.Name}'s Gold\t: {player.Gold}Gold\t\t│");
            Console.WriteLine("└───────────────────────────────────────┘");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();

        }
    }
}
