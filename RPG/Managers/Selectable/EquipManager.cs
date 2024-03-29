﻿using Entities;
using Equipments;
using Processors;

namespace Managers
{
    
    class EquipManager
    {
        List<Equip> Equipped;
        List<Equip> EInven;
        public SelectProcessor<Equip> EinveSelP;
        Player player;

        public EquipManager()
        {
            Equipped = new List<Equip>();
            EInven = new List<Equip>();
            EinveSelP = new SelectProcessor<Equip>(EInven);
        }

        void InitEquipped()
        {
            for (int i = 0; i < (int)Parts.Max; i++)
                Equipped.Add(new Empty(player, (Parts)i));
        }
        public void SetPlayer(Player player) 
        {
            this.player = player;
            InitEquipped();
        }

        public void SetSelected()
        {
            if (EInven.Count > 0)
                EInven.First().IsSelected = true;
            else return;
        }

        public void Equips(Equip equip)
        {
            if (player.LV < equip.equipLV)
            {
                Console.WriteLine("Not Enough Level");
                Console.ReadLine();
                return;
            }
            if (Equipped[(int)equip.part].isEquipped)
                UnEquip(Equipped[(int)equip.part]);

            equip.isEquipped = true;
            Equipped[(int)equip.part] = equip;
            EStatUp(equip);
            RemoveEinven(equip);

            Cleaner.CleanBox();
            Console.WriteLine($"{equip.Name} Equipped!");
            Console.ReadLine();
            if (equip.part == Parts.Weapon)
                Console.WriteLine($"ATK Increase at {equip.Value}!");
            else
                Console.WriteLine($"DEF Increase at {equip.Value}!");
            Console.ReadLine();
        }

        public void UnEquip(Equip equip)
        {
            if (equip.Name == "Empty") return;

            Equipped[(int)equip.part].isEquipped = false;
            UEStatDown(Equipped[(int)equip.part]);
            EInven.Add(Equipped[(int)equip.part]);
            Equipped[(int)equip.part] = new Empty(player, equip.part);

            equip.isEquipped = true;
            Equipped[(int)equip.part] = equip;
            SetSelected();

            Cleaner.CleanBox();
            Console.WriteLine($"{equip.Name} UnEquipped.");
            Console.ReadLine();
            if (equip.part == Parts.Weapon)
                Console.WriteLine($"ATK Decreased about {equip.Value}");
            else
                Console.WriteLine($"DEF Decreased about {equip.Value}");
            Console.ReadLine();
        }

        public void EStatUp(Equip equip)
        {
            if (equip.part == Parts.Weapon)
                player.Atk += equip.Value;
            else
                player.Def += equip.Value;
        }

        public void UEStatDown(Equip equip)
        {
            if (equip.part == Parts.Weapon)
                player.Atk -= equip.Value;
            else
                player.Def -= equip.Value;
        }

        public void EinvenAdd(Equip equip)
        {
            EInven.Add(equip);
            SetSelected();
        }



        public void ShowEquipped()
        {
            Console.WriteLine("\n┌───────────────────────┐");
            for (int i = 0; i < (int)Parts.Max; i++)
            {
                Console.Write($"│{(Parts)i}\t: ");
                if (Equipped[i].Name == "Empty" )
                    Console.WriteLine("Empty\t\t│");
                else if (Equipped[i].Name == "Sabor")
                    Console.WriteLine("Sabor\t\t│");
                else
                    Console.WriteLine($"{Equipped[i].Name}\t│");
            }
            Console.WriteLine("└───────────────────────┘");
            Console.ReadLine();
            Console.Clear();
        }

        public List<Equip> GetEinven() { return EInven; }

        public List<Equip> GetEquipped() { return Equipped; }

        public void RemoveEinven(Equip equip)
        {
            EInven.Remove(equip);
        }

    }
    
}
