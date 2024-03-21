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
            Cleaner.CleanBox();
            Console.WriteLine("Welcome! Rest for 100Gold! Wanna Rest?");
            Console.WriteLine($"You Have {player.Gold}Gold");
            if (TB.Yes())
                inn.Rest();

        }
    }
}
/*
 yes or no
 
 */
