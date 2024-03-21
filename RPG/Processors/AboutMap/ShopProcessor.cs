using Entities;
using Equipments;
using Managers;
using Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usable;

namespace Processors
{
    
    class ShopProcessor
    {
        Merchant shop;
        Player player;
        ItemManager iM;
        EquipManager eM;
        MainProcessor mP;
        List<Item> itemlist;
        public SelectProcessor<Item> itemSelP;
        List<Equip> LowEquipList;
        public SelectProcessor<Equip> lowEquipSelP;

        List<Equip> NormalEquipList;
        public SelectProcessor<Equip> normalEquipSelP;

        List<Equip> HighEquipList;
        public SelectProcessor<Equip> highEquipSelP;

        public ShopProcessor(Player player, ItemManager iM, EquipManager eM, MainProcessor mP)
        {
            this.player = player;
            this.iM = iM;
            this.eM = eM;
            this.mP = mP;
            shop = new Merchant(iM, eM, player);
            itemlist = shop.GetItemList();
            LowEquipList= shop.GetLowEquipList();
            NormalEquipList = shop.GetNormalEquipList();
            HighEquipList = shop.GetHighEquipList();
            itemSelP = new SelectProcessor<Item>(itemlist, true);
            lowEquipSelP = new SelectProcessor<Equip> (LowEquipList, true);
            normalEquipSelP = new SelectProcessor<Equip>(NormalEquipList, true);
            highEquipSelP = new SelectProcessor<Equip> (HighEquipList,true);
        }
        public Player GetPlayer() { return player; }
        public Merchant GetShop() { return shop; }

        public void Buy()
        {

        }

        /*
        public void Shop()
        {
            ShopSelect();
        }
       */
        /*
        void ShopSelect()
        {
            int num = 0;
            while (true)
            {
                Cleaner.Clear();
                Console.WriteLine($"1. Buy 2. Sell 0. Exit \t\t\t Remaining Gold : {player.Gold}");
                num = InputProcessor.Input(3);
                switch (num)
                {
                    case 0:
                        Cleaner.Clear();
                        return;

                    case 1:
                        Buy();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("1. Sell Item         2. Sell EquipManager\t\t 0. Exit");
                        Sell(InputProcessor.Input(2));
                        break;

                    default:
                        Console.WriteLine("Wrong Input. TryAgain");
                        Console.ReadLine();
                        ShopSelect();
                        break;
                }
            }
        }
        */
        /*
        void Buy()
        {
            Console.Clear();
            shop.ShowList();
            Console.WriteLine($"\nRemaining Gold : {player.Gold}g \n0. Exit");
            int num = InputProcessor.Input(shop.GetSellables().Count);
            if (num == 0) { return; }

            ISellable sellable = shop.GetSellable(num - 1);
            Console.Clear();
            shop.ShowList();
            Console.WriteLine();
            Console.WriteLine($"{sellable.Name}'s Price is {sellable.Price}gold. is That OK? \n 1. OK 2. No");

            num = InputProcessor.Input(2);

            if (num == 1)
            {
                Console.Clear();
                shop.ShowList();
                Console.WriteLine();
                shop.Purchase(player, sellable);
                Console.WriteLine($"You Got {sellable.Name}! ");
                Console.WriteLine($"Remaining Gold : {player.Gold}");
                Console.ReadLine();
            }
            Buy();
        }
        */
        /*
        void Sell(int num)
        {
            Console.Clear();
            if (num == 1)
            {
                if (iM.inventory.Count <= 0)
                {
                    Console.WriteLine("You Do Not Have Items!");
                    Console.ReadLine();
                    return;
                }

                iM.InvenIndicator();
                Console.WriteLine("\nSelect Item You want to Sell\t\t 0. Exit");


                num = InputProcessor.Input(iM.inventory.Count);
                if (num == 0) { return; }

                Console.WriteLine($"{iM.inventory[num - 1].Name}'s Sell Price is " +
                    $"{iM.inventory[num - 1].Price / 5}gold. is that OK? \n 1. OK 2. NO");

                num = InputProcessor.Input(2);

                if (num == 1)
                    shop.Sell(player, iM.inventory[num - 1]);
                else
                    Sell(1);
            }
            else if (num == 2)
            {
                if (eM.GetEinven().Count <= 0)
                {
                    Console.WriteLine("You Do Not Have EquipManager!");
                    Console.ReadLine();
                    return;
                }
                eM.EinvenIndicator();
                Console.WriteLine("Use EquipManager You want to Sell\t\t 0. Exit");
                num = InputProcessor.Input(iM.inventory.Count);

                if (num == 0) { return; }

                shop.Sell(player, eM.GetEinven()[num - 1]);
            }
            else
                return;
        }
        */

    }
}
