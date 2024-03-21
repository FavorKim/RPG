using Entities;
using Equipments;
using Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            EinveSelP = new SelectProcessor<Equip>(EInven, true);
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


        //Will be Deleted
        public void EinvenIndicator()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n┌───Equipments Inventory──┐");
            for (int i = 0; i < EInven.Count; i++)
                Console.WriteLine($"│\t{i + 1}. {EInven[i].Name}\t  │");
            Console.WriteLine("└─────────────────────────┘");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        public void ShowEquipped()
        {
            Console.WriteLine("\n┌─────────────────────────┐");
            for (int i = 0; i < (int)Parts.Max; i++)
            {
                if (Equipped[i].Name == "Empty" )
                    Console.WriteLine("│\t   Empty\t  │");
                else
                    Console.WriteLine($"│\t   {Equipped[i].Name}\t  │");
                //Console.WriteLine
            }
            Console.WriteLine("└─────────────────────────┘");
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
