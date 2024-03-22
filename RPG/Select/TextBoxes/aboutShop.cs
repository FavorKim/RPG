using Entities;
using Equipments;
using Managers;
using Processors;
using RPG.Select.Selectors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Usable;

namespace Selectable
{
    class BuyTB : TextBox
    {
        ShopSelector ss;
        public BuyTB(ShopSelector ss)
        {
            Name = "Buy";
            this.ss = ss;
        }

        public override void Use()
        {
            ss.buySP.SelectVoid();
        }
    }
    class SellTB : TextBox
    {
        ShopSelector ss;
        public SellTB(ShopSelector ss)
        {
            Name = "Sell";
            this.ss = ss;
        }

        public override void Use()
        {
            ss.sellSP.SelectVoid();
        }
    }
    class PrevTB : TextBox
    {
        SelectProcessor<TextBox> selP;
        public PrevTB(SelectProcessor<TextBox> selP)
        {
            Name = "Prev(ESC)";
            this.selP = selP;
        }
        public override void Use()
        {
            return;
        }
    }

    class BuyItemTB : TextBox
    {
        Merchant.Shop shop;
        Player player;
        ItemManager iM;
        public BuyItemTB(Merchant.Shop shop, Player player, ItemManager iM)
        {
            this.shop = shop;
            Name = "Buy Item";
            this.player = player;
            this.iM = iM;
        }
        public override void Use()
        {
            ShopSelector.PrintStat(player);
            Cleaner.CleanBox();
            Item temp = (Item)shop.itemSelP.SelectReturn();

            if (temp == null) return;

            temp.Description();
            Console.WriteLine("\nDo You Really Want to Buy?\n");

            if (Yes())
            {
                if (player.Gold >= temp.Price)
                    Console.WriteLine($"You Bought {temp.Name} at {temp.Price}Gold");
                shop.Purchase(player, temp);
                Console.ReadLine();
                Console.Clear();
            }

            else
                return;
        }
    }
    class BuyEquipTB : TextBox
    {
        SelectProcessor<TextBox> selP;

        public BuyEquipTB(SelectProcessor<TextBox> lnSelP)
        {
            Name = "Buy Equipments";
            selP = lnSelP;
        }
        public override void Use()
        {
            selP.SelectVoid();
        }
    }

    class BuyLowEquipTB : TextBox
    {
        Player player;
        Merchant.Shop shop;
        public BuyLowEquipTB(Merchant.Shop shop, Player player)
        {
            this.shop = shop;
            Name = "Buy Low Equipment";
            this.player = player;
        }
        public override void Use()
        {
            ShopSelector.PrintStat(player);
            Cleaner.CleanBox();

            Equip temp = (Equip)shop.lowEquipSelP.SelectReturn();

            if (temp == null) return;
            temp.ShowStat();
            Console.WriteLine("\nDo You Really Want to Buy?\n");

            if (Yes())
            {
                if (player.Gold >= temp.Price)
                    Console.WriteLine($"You Bought {temp.Name} at {temp.Price}Gold");
                shop.Purchase(player, temp);
                Console.ReadLine();
                Console.Clear();
            }
            else
                return;
        }
    }
    class BuyNormalEquipTB : TextBox
    {
        Merchant.Shop shop;
        Player player;
        public BuyNormalEquipTB(Merchant.Shop shop, Player player)
        {
            this.shop = shop;
            Name = "Buy Normal Equipment";
            this.player = player;
        }
        public override void Use()
        {
            ShopSelector.PrintStat(player);
            Cleaner.CleanBox();

            Equip temp = (Equip)shop.normalEquipSelP.SelectReturn();

            if (temp == null) return;
            temp.ShowStat();
            Console.WriteLine("\nDo You Really Want to Buy?\n");

            if (Yes())
            {
                if (player.Gold >= temp.Price)
                    Console.WriteLine($"You Bought {temp.Name} at {temp.Price}Gold");
                shop.Purchase(player, temp);
                Console.ReadLine();
                Console.Clear();
            }
            else
                return;
        }
    }
    class BuyHighEquipTB : TextBox
    {
        Merchant.Shop shop;
        Player player;
        public BuyHighEquipTB(Merchant.Shop shop, Player player)
        {
            this.shop = shop;
            Name = "Buy High Equipment";
            this.player = player;
        }
        public override void Use()
        {

            Cleaner.CleanBox();
            ShopSelector.PrintStat(player);
            Equip temp = (Equip)shop.highEquipSelP.SelectReturn();

            if (temp == null) return;
            temp.ShowStat();
            Console.WriteLine("\nDo You Really Want to Buy?\n");

            if (Yes())
            {
                if (player.Gold >= temp.Price)
                    Console.WriteLine($"You Bought {temp.Name} at {temp.Price}Gold");
                shop.Purchase(player, temp);
                Console.ReadLine();
                Console.Clear();
            }
            else
                return;
        }
    }

    class SellItemTB : TextBox
    {
        ItemManager iM;
        Item temp;
        Player player;
        public SellItemTB(ItemManager iM, Player player)
        {
            Name = "Sell Item";
            this.iM = iM;
            this.player = player;
        }
        public override void Use()
        {
            ShopSelector.PrintStat(player);
            Cleaner.CleanBox();
            temp = (Item)iM.selectP.SelectReturn();
            if (temp != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"!! {temp.Name}'s Resell Price is {temp.Price / 5} Gold !!");
                Console.ResetColor();
                Console.WriteLine("Do You Really Want to Buy?\n");
                if (Yes())
                {
                    Console.WriteLine($"\n Sold {temp.Name} at {temp.Price / 5}Gold");
                    Sell(temp);
                    Console.ReadLine();
                }
            }

        }
        void Sell(Item item)
        {
            if (item.Consume > 1)
                item.Consume--;
            else
                player.Inventory.Remove(item);
            player.Gold += item.Price / 5;
        }
    }
    class SellEquipTB : TextBox
    {
        EquipManager eM;
        Equip temp;
        Player player;
        public SellEquipTB(EquipManager eM, Player player)
        {
            this.eM = eM;
            this.player = player;
            Name = "Sell Equipment";
        }
        public override void Use()
        {
            ShopSelector.PrintStat(player);
            Cleaner.CleanBox();
            temp = (Equip)eM.EinveSelP.SelectReturn();
            if (temp != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"!! {temp.Name}'s Sell Price is {temp.Price / 5} Gold !!");
                Console.ResetColor();
                Console.WriteLine("Do You Really Want to Sell?");
                if (Yes())
                {
                    Console.WriteLine($"Sold {temp.Name} at {temp.Price / 5}Gold");
                    Sell(temp);
                    Console.ReadLine();
                    Console.Clear();
                }
            }


        }
        void Sell(Equip equip)
        {
            player.EInven.Remove(equip);
            player.Gold += equip.Price / 5;
        }
    }
}
