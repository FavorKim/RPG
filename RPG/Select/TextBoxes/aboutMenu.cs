using Entities;
using Equipments;
using Managers;
using Processors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Selectable
{
    class invenTB : TextBox
    {
        ItemManager iM;
        public invenTB(ItemManager iM)
        {
            this.iM = iM;
            Name = "Inventory";
        }
        public override void Use()
        {
            iM.selectP.SelectVoid();
        }
    }
    class StatTB : TextBox
    {
        IndicateProcess iP;
        Player player;
        public StatTB(IndicateProcess p, Player player)
        {
            iP = p;
            Name = "Player Status";
            this.player = player;
        }
        public override void Use()
        {
            iP.Status();
        }
    }
    class SkillsTB : TextBox
    {
        SkillManager sM;
        public SkillsTB(SkillManager sM)
        {
            this.sM = sM;
            Name = "SkillList";
        }
        public override void Use()
        {
            sM.selP.SelectReturn();
        }
    }
    class EquipTB : TextBox
    {
        EquipManager eM;
        public EquipTB(EquipManager eM)
        {
            this.eM = eM;
            Name = "EquipInventory";
        }
        public override void Use()
        {
            eM.SetSelected();
            Equip temp;
            temp = (Equip)eM.EinveSelP.SelectReturn();
            if (temp == null)
                return;
            temp.Use();
        }
    }
    class EquippedTB : TextBox
    {
        EquipManager eM;
        public EquippedTB(EquipManager eM)
        {
            this.eM = eM;
            Name = "Equipped Equipments";
        }
        public override void Use()
        {
            eM.ShowEquipped();
        }
    }
}
