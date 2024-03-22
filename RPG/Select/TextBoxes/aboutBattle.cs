using Entities;
using Managers;
using Processors;
using RPG.Select.Selectors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Usable;

namespace Selectable
{
    class BattleSkillTB : TextBox
    {
        SkillManager sM;
        Player player;
        BattleSelector prev;
        public BattleSkillTB(Player player, SkillManager sM, BattleSelector prev)
        {
            this.player = player;
            this.sM = sM;
            Name = "Skill";
            this.prev = prev;
        }

        public SkillManager GetSM() { return sM; }

        public override void Use()
        {
            Skill stemp;
            stemp = (Skill)sM.selP.SelectReturn();
            if (stemp != null)
            {
                if (player.CurMP < stemp.Consume)
                {
                    Console.WriteLine("Can't Use Skill.");
                    Console.ReadLine();
                    prev.Action();
                }
                else
                {
                    if (stemp is Rage && player.IsRaged)
                    {
                        Console.WriteLine("You are already Raged!");
                        Console.ReadLine();
                        prev.Action();
                    }
                    else
                        stemp.Use();
                }
            }
            else
                prev.Action();
        }
    }
    class BattleItemTB : TextBox
    {
        ItemManager iM;
        BattleSelector prev;
        public BattleItemTB(ItemManager iM, BattleSelector prev)
        {
            this.iM = iM;
            this.prev = prev;
            Name = "Inventory";
        }

        public override void Use()
        {
            Item temp;
            temp = (Item)iM.selectP.SelectReturn();
            if (temp != null)
                temp.Use();
            else
                prev.Action();
        }
    }
    class BattleAttackTB : TextBox
    {
        Player player;
        Monster mon;
        public BattleAttackTB(Player player)
        {
            this.player = player;
            Name = "Attack";
        }

        public void SetMon(Monster mon) { this.mon = mon; }

        public override void Use()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            player.Attack(mon);
            Console.ResetColor();
            IndicateProcess.BattleIndicator(player, mon);
        }

    }
}
