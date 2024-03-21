﻿using Entities;
using Managers.Selectable;
using Processors;
using Selectable;
using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;

namespace Equipments
{
    public enum Parts
    {
        Weapon, Head, Plate, Pants, Feet, Max
    }

    class Equip : ISellable, ISelectable
    {
        public Parts part { get; set; }
        public int Value { get; protected set; }
        public int Price { get; set; }
        public int equipLV { get; protected set; }
        public bool isEquipped { get; set; }
        public string Name { get; set; }
        public bool isItem() { return false; }
        public bool IsSelected { get; set; }
        Player player;


        public void Use()
        {
            player.eM.Equips(this);
        }

        public Equip(Player player)
        {
            isEquipped = false;
            this.player = player;
        }

        public void ShowStat()
        {
            Cleaner.CleanBox();
            Console.WriteLine($"\t{Name}\t");
            Console.WriteLine("┌───────────────────────┐");
            Console.WriteLine($"\tParts : {part}\t");
            if (part == Parts.Weapon)
                Console.WriteLine($"│\tATK : {Value}");
            else
                Console.WriteLine($"\tDEF : {Value}");

            Console.WriteLine($"\tEquip Level :{equipLV}");
            Console.WriteLine("└───────────────────────┘");
        }
    }
    
    class Empty : Equip
    {
        public Empty(Player player, Parts parts) : base(player)
        {
            this.part = parts;
            Value = 0;
            Name = "Empty";
            isEquipped = true;
        }
    }
}