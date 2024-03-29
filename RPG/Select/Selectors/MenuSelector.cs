﻿using Entities;
using Managers;
using Processors;
using Selectable;

namespace Select
{
    class MenuSelector
    {
        List<TextBox> selects;
        public SelectProcessor<TextBox> selP;
        ItemManager iM;
        IndicateProcess iP;
        SkillManager sM;
        EquipManager eM;
        invenTB inven;
        StatTB stat;
        SkillsTB skil;
        EquipTB equip;
        EquippedTB equipped;
        Player player;
        public MenuSelector(SkillManager sM, ItemManager iM, IndicateProcess iP, EquipManager eM,Player player)
        {
            this.sM = sM;
            this.iM = iM;
            this.iP = iP;
            this.eM = eM;
            this.player = player;
            inven = new invenTB(iM);
            stat = new StatTB(iP,player);
            skil = new SkillsTB(sM);
            equip = new EquipTB(eM);
            equipped = new EquippedTB(eM);
            selects = new List<TextBox>();
            Init();

            selP = new SelectProcessor<TextBox>(selects);
        }
        void Init()
        {
            selects.Add(inven);
            selects.Add(stat);
            selects.Add(skil);
            selects.Add(equip);
            selects.Add(equipped);
            selects[0].IsSelected = true;
        }
    }

    
}
