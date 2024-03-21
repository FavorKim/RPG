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
        public InnSelector(Inn inn, Player player)
        {
            TB = new YesNO();
            this.inn = inn;
            this.player = player;
        }

        public void Welcome()
        {
            
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
