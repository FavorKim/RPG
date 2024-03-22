using Entities;
using Managers;
using Managers.Selectable;
using Processors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Select.Selectors
{
    class BattleSelector
    {
        List<TextBox> battle;
        public SelectProcessor<TextBox> bSelP;

        BattleAttackTB atk;
        BattleSkillTB skil;
        BattleItemTB inv;



        Player player;
        SkillManager sM;
        ItemManager iM;

        public BattleSelector(Player player, SkillManager sM, ItemManager iM)
        {
            this.player = player;
            this.sM = sM;
            this.iM = iM;
            Init();
        }


        public void Action()
        {
            ISelectable temp = bSelP.SelectReturn();
            if (temp == null) Action();
            else
            {
                if (temp == GetATK())
                {
                    temp.Use();
                    return;
                }
                else
                    temp.Use();
            }
        }
        public BattleAttackTB GetATK() { return atk; }
        public BattleSkillTB GetSKil() {  return skil; }

        public void SetMon(Monster mon)
        {
            atk.SetMon(mon);
        }
        void Init()
        {
            battle = new List<TextBox>();
            bSelP = new SelectProcessor<TextBox>(battle);

            atk = new BattleAttackTB(player);
            skil = new BattleSkillTB(player, sM, this);
            inv = new BattleItemTB(iM, this);

            battle.Add(atk);
            battle.Add(skil);
            battle.Add(inv);
        }
    }
}
