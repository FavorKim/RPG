using Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usable;

namespace Managers
{
    class ItemManager
    {
        public SelectProcessor<Item> selectP;
        public List<Item> inventory;
        public ItemManager(List<Item> inventory) 
        {
            this.inventory = inventory;
            selectP = new SelectProcessor<Item>(inventory, true);
        }
        
        public void SetSelected()
        {
            if(inventory.Count > 0)
                inventory.First().IsSelected = true;
        }
        public void AddInven(Item temp)
        {
            Item item = (Item)temp.GetDeep();
            
            EmptyRemover();

            int already = 0;
            int index = 0;
            if (inventory.Count == 0)
            {
                
                inventory.Add(item);
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].Name == item.Name)
                    {
                        already++;
                        index = i;
                    }
                }
                if (already != 0)
                    inventory[index].Consume++;
                else
                {
                    inventory.Add(item);
                }
            }
            SetSelected();
        }
        public void EmptyRemover()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Consume == 0)
                {
                    inventory.Remove(inventory[i]);
                    i--;
                }
            }
        }


        //Will be Deleted
        public void InvenIndicator()
        {
            EmptyRemover();

            if (inventory.Count == 0)
            {
                Console.WriteLine("Inventory is Empty");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("─────────────Inventory───────────");
            Console.ResetColor();
            Console.ReadLine();
            /*
            for (int i = 0; i < inventory.Count; i++)
                Console.WriteLine($"│\t{i + 1}. {inventory[i].Name} : {inventory[i].Consume}ea\t  │");
            Console.WriteLine("└─────────────────────────────────┘");
            */
        }


    }
}
