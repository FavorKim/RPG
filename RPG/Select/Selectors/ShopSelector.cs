﻿using Entities;
using Managers;
using Processors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Select.Selectors
{
    class ShopSelector
    {
        List<TextBox> first;// buy, sell, exit
        public SelectProcessor<TextBox> firstSP;
        List<TextBox> Buy;// buyitem buyequip, prev
        public SelectProcessor<TextBox> buySP;
        List<TextBox> Sell;// sellitem sellequip prev
        public SelectProcessor<TextBox> sellSP;

        List<TextBox> equipsList;
        public SelectProcessor<TextBox> equipsSelP;

        ShopProcessor sP;
        Player player;
        ItemManager iM;
        EquipManager eM;

        public ShopSelector(Player player, ShopProcessor sP, ItemManager iM, EquipManager eM)
        {
            this.player= player;
            this.iM= iM;
            this.eM= eM;
            this.sP= sP;
            Init();
        }

        public static void PrintStat(Player player)
        {
            Console.SetCursorPosition(0,0);
            Console.WriteLine($"Your Remaining Gold : {player.Gold}");
            Console.WriteLine($"Your Level : {player.LV}");
        }

        void Init()
        {
            first = new List<TextBox>();
            firstSP = new SelectProcessor<TextBox>(first, true);

            Buy = new List<TextBox>();
            buySP = new SelectProcessor<TextBox>(Buy, true);

            Sell = new List<TextBox>();
            sellSP = new SelectProcessor<TextBox>(Sell, true);

            equipsList = new List<TextBox>();
            equipsSelP = new SelectProcessor<TextBox>(equipsList, true);

           
            BuyTB buyTB = new BuyTB(this);
            SellTB sellTB = new SellTB(this);

            first.Add(buyTB);
            first.Add(sellTB);



            BuyItemTB buyItemTB = new BuyItemTB(sP, player);
            BuyEquipTB buyEquipTB = new BuyEquipTB(equipsSelP);
            Buy.Add(buyItemTB);
            Buy.Add(buyEquipTB);

            SellItemTB sellItemTB = new SellItemTB(iM, player);
            SellEquipTB sellEquipTB = new SellEquipTB(eM, player);
            Sell.Add(sellItemTB);
            Sell.Add(sellEquipTB);


            BuyLowEquipTB low = new BuyLowEquipTB(sP, player);
            BuyNormalEquipTB nor = new BuyNormalEquipTB(sP,player);
            BuyHighEquipTB high = new BuyHighEquipTB(sP, player);
            equipsList.Add(low);
            equipsList.Add(nor);
            equipsList.Add(high);
            
        }
    }
}


