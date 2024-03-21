using Managers;
using Processors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public MenuSelector(SkillManager sM, ItemManager iM, IndicateProcess iP, EquipManager eM)
        {
            this.sM = sM;
            this.iM = iM;
            this.iP = iP;
            this.eM = eM;

            inven = new invenTB(iM);
            stat = new StatTB(iP);
            skil = new SkillsTB(sM);
            equip = new EquipTB(eM);
            equipped = new EquippedTB(eM);
            selects = new List<TextBox>();
            Init();

            selP = new SelectProcessor<TextBox>(selects, true);
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
