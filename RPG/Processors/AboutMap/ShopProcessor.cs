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
        EquipManager eM;
        MainProcessor mP;
        ItemManager iM;

        List<Item> itemlist;
        public SelectProcessor<Item> itemSelP;

        List<Equip> LowEquipList;
        public SelectProcessor<Equip> lowEquipSelP;

        List<Equip> NormalEquipList;
        public SelectProcessor<Equip> normalEquipSelP;

        List<Equip> HighEquipList;
        public SelectProcessor<Equip> highEquipSelP;

        public ShopProcessor(Player player, EquipManager eM, MainProcessor mP, ItemManager iM)
        {
            this.player = player;
            this.eM = eM;
            this.mP = mP;
            this.iM = iM;
            shop = new Merchant(eM, player, iM);
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

      


    }
}
