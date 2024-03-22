using Entities;
using Managers;
using Processors;
using Selectable;
using Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipments;
using Usable;

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

        Shop shop;
        Player player;
        ItemManager iM;
        EquipManager eM;

        public ShopSelector(Player player, Shop shop, ItemManager iM, EquipManager eM)
        {
            this.player = player;
            this.iM = iM;
            this.eM = eM;
            this.shop = shop;
            Init();
        }

        public static void PrintStat(Player player)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Your Remaining Gold : {player.Gold}");
            Console.WriteLine($"Your Level : {player.LV}");
        }

        void Init()
        {
            first = new List<TextBox>();
            firstSP = new SelectProcessor<TextBox>(first);

            Buy = new List<TextBox>();
            buySP = new SelectProcessor<TextBox>(Buy);

            Sell = new List<TextBox>();
            sellSP = new SelectProcessor<TextBox>(Sell);

            equipsList = new List<TextBox>();
            equipsSelP = new SelectProcessor<TextBox>(equipsList);


            BuyTB buyTB = new BuyTB(this);
            SellTB sellTB = new SellTB(this);

            first.Add(buyTB);
            first.Add(sellTB);



            BuyItemTB buyItemTB = new BuyItemTB(shop, player, iM);
            BuyEquipTB buyEquipTB = new BuyEquipTB(equipsSelP);
            Buy.Add(buyItemTB);
            Buy.Add(buyEquipTB);

            SellItemTB sellItemTB = new SellItemTB(iM, player);
            SellEquipTB sellEquipTB = new SellEquipTB(eM, player);
            Sell.Add(sellItemTB);
            Sell.Add(sellEquipTB);


            BuyLowEquipTB low = new BuyLowEquipTB(shop, player);
            BuyNormalEquipTB nor = new BuyNormalEquipTB(shop, player);
            BuyHighEquipTB high = new BuyHighEquipTB(shop, player);
            equipsList.Add(low);
            equipsList.Add(nor);
            equipsList.Add(high);

        }

       
    }
}


