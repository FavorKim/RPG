using Entities;
using Equipments;
using Manager;
using System.Diagnostics;
using Usable;

namespace Shop
{
    class Merchant
    {
        // List<Item> itemList = new List<Item>();
        // List<Equip> equipList = new List<Equip>();
        ItemManager iM;
        EquipManager eM;

        public Merchant(ItemManager iM, EquipManager eM)
        {
            this.iM = iM;
            this.eM = eM;
            sellableList.Add(new LowPortion());
            sellableList.Add(new NormalPortion());
            sellableList.Add(new HighPortion());
            sellableList.Add(new LowPlate());
            sellableList.Add(new LowHelm());

        }

        List<ISellable> sellableList = new List<ISellable>();

        public List<ISellable> GetSellables() { return sellableList; }
        public ISellable GetSellable(int index) { return sellableList[index]; }
        

        public void Purchase(Player player, ISellable sellable)
        {
            if (sellable == null) return;
            if (sellable.Price > player.Gold)
                Console.WriteLine("Not Enough Gold");
            else
            {
                player.Gold -= sellable.Price;
                if (sellable.isItem())
                    iM.AddInven((Item)sellable);
                else
                {
                    eM.EinvenAdd((Equip)sellable);
                    sellableList.Remove(sellable);
                }
            }
        }
        
        public void ShowList()
        {
            for (int i = 0; i < sellableList.Count; i++)
                Console.WriteLine($"{i + 1}. {sellableList[i].Name} : {sellableList[i].Price}gold");
        }
        
        public void Sell(Player player, ISellable sellable)
        {
            player.Gold += (sellable.Price / 5);
            Console.WriteLine($"Sold {sellable.Name}. Gold : {player.Gold} ");
            if (sellable.isItem())
                iM.inventory.Remove((Item)sellable);
            else
                eM.RemoveEinven((Equip)sellable);

            Console.ReadLine();
        }
        
    }

}
