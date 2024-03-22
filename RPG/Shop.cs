using Entities;
using Equipments;
using Managers;
using Processors;
using System.Diagnostics;
using Usable;

namespace Merchant
{
    class Shop
    {
        List<Item> itemList = new List<Item>();
        List<Equip> lowEquipList = new List<Equip>();
        List<Equip> NormalEquipList = new List<Equip>();
        List<Equip> HighEquipList = new List<Equip>();

        public SelectProcessor<Item> itemSelP;
        public SelectProcessor<Equip> lowEquipSelP;
        public SelectProcessor<Equip> normalEquipSelP;
        public SelectProcessor<Equip> highEquipSelP;

        ItemManager iM;
        EquipManager eM;
        Player player;


        public Shop( EquipManager eM, Player player, ItemManager iM)
        {
            this.iM = iM;
            this.eM = eM;
            this.player = player;
            itemSelP = new SelectProcessor<Item>(itemList, true);
            lowEquipSelP = new SelectProcessor<Equip>(lowEquipList, true);
            normalEquipSelP = new SelectProcessor<Equip>(NormalEquipList, true);
            highEquipSelP = new SelectProcessor<Equip> (HighEquipList, true);

            AddItems();

            itemList.First().IsSelected = true;
            lowEquipList.First().IsSelected = true;
            NormalEquipList.First().IsSelected = true;
        }

        public List<Item> GetItemList() { return itemList; }
        public List<Equip> GetLowEquipList() { return lowEquipList; }
        public List<Equip> GetNormalEquipList() { return NormalEquipList; }
        public List<Equip> GetHighEquipList() { return HighEquipList; }

       void AddItems()
        {
            itemList.Add(new LowPortion(player, iM));
            itemList.Add(new NormalPortion(player, iM));
            itemList.Add(new HighPortion(player, iM));

            lowEquipList.Add(new LowPlate(player));
            lowEquipList.Add(new LowHelm(player));
            lowEquipList.Add(new LowBoots(player));
            lowEquipList.Add(new LowPants(player));
            lowEquipList.Add(new OldBastard(player));

            NormalEquipList.Add(new NormalBoots(player));
            NormalEquipList.Add(new NormalPants(player));
            NormalEquipList.Add(new NormalPlate(player));
            NormalEquipList.Add(new NormalHelm(player));
            NormalEquipList.Add(new Sabor(player));

            HighEquipList.Add(new HighBoots(player));
            HighEquipList.Add(new HighPants(player));
            HighEquipList.Add(new HighPlate(player));
            HighEquipList.Add(new HighHelm(player));
            HighEquipList.Add(new TweiHander(player));
        }

        public void Purchase(Player player, ISellable sellable)
        {
            if (sellable == null) return;

            if (sellable.Price > player.Gold)
                Console.WriteLine("Not Enough Gold");
            else
            {
                player.Gold -= sellable.Price;
                if (sellable is Item)
                {
                    iM.AddInven((Item)sellable);
                }
                else if (sellable is Equip)
                {
                    if (lowEquipList.Contains(sellable))
                    {
                        eM.EinvenAdd((Equip)sellable.GetDeep());
                        if (lowEquipList.Count > 0)
                            lowEquipList.First().IsSelected = true;
                    }
                    else if (NormalEquipList.Contains(sellable))
                    {
                        eM.EinvenAdd((Equip)sellable.GetDeep());
                        if (NormalEquipList.Count > 0)
                            NormalEquipList.First().IsSelected = true;
                    }
                    else if (HighEquipList.Contains(sellable))
                    {
                        eM.EinvenAdd((Equip)sellable.GetDeep());
                        if (HighEquipList.Count > 0)
                            HighEquipList.First().IsSelected = true;
                    }
                }
            }
        }

        

        public void Sell(Player player, ISellable sellable)
        {
            player.Gold += (sellable.Price / 5);
            if (sellable.isItem())
                iM.inventory.Remove((Item)sellable);
            else
                eM.RemoveEinven((Equip)sellable);
        }

    }

}
