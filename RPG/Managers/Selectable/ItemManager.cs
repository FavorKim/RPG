﻿using Processors;
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
            selectP = new SelectProcessor<Item>(inventory);
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




    }
}
