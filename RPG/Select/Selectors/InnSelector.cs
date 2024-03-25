using Entities;
using Mapper;
using Processors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selectors
{
    class InnSelector
    {
        Inn inn;
        YesNO TB;
        Player player;
        IndicateProcess iP;
        public InnSelector(Inn inn, Player player, IndicateProcess iP)
        {
            TB = new YesNO();
            this.inn = inn;
            this.player = player;
            this.iP = iP;
        }

        public void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n┌───────────────────────────────────────┐");
            if (player.CurHP >= 100 && player.MaxHP >= 100)
                Console.WriteLine($"│\t{player.Name}'s HP\t: {player.CurHP}/{player.MaxHP}\t│");
            else
                Console.WriteLine($"│\t{player.Name}'s HP\t: {player.CurHP}/{player.MaxHP}\t\t│");
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
            Cleaner.CleanBox();
            Console.WriteLine("Welcome! Rest for 100Gold! Wanna Rest?");
            Console.WriteLine($"You Have {player.Gold}Gold");
            if (TB.Yes())
                inn.Rest();
        }
    }
}
