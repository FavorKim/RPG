using Entities;
using Managers;
using Managers.Selectable;
using RPG.Select.Selectors;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usable;

namespace Processors
{
    class BattleProcessor
    {
        Player player;
        Monster mon;
        MonsterManager monM;
        ItemManager iM;
        MainProcessor mainP;
        SkillManager sM;
        BattleSelector bsel;


        public delegate void BattleOver();
        public event BattleOver OnBattleOver;


        public BattleProcessor(Player player, MonsterManager monM, ItemManager iM, MainProcessor mainP, SkillManager sM, BattleSelector bsel)
        {
            this.player = player;
            this.monM = monM;
            this.iM = iM;
            this.mainP = mainP;
            OnBattleOver += player.CheckLevelUp;
            this.sM = sM;
            this.bsel = bsel;

        }

        public void BattleResult()
        {
            if (mon.IsDead())
            {
                Console.WriteLine("WIN!");
                Console.ReadLine();
                player.curEXP += mon.ExpPK;
                player.Gold += mon.GoldPK;
                OnBattleOver();
            }
            else
            {
                Console.WriteLine("Lose...");
                player.Lose();
                Console.ReadLine();
            }
        }

        public void Battle(string monName)
        {
            Encounter(monM.GetMonster(monName));

            while (!mon.IsDead() && !player.IsDead())
            {

                BattleSelect();
                if (!mon.IsDead())
                {
                    MonsterTurn();
                }
            }
            BattleResult();
            Cleaner.Clear();
        }
        public void MonsterTurn()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            mon.Attack(player);
            Console.ResetColor();
        }
        public void Encounter(Monster mon)
        {
            this.mon = mon;
            this.mon.Init();
            bsel.SetMon(mon);
            SkillDestSet(mon);
        }
        public void SkillDestSet(Monster mon)
        {
            foreach (Skill s in player.GetSkills())
            {
                if (s.IsAttack)
                    s.SetDest(mon);
                else
                    s.SetDest(player);
            }
        }
        public void BattleSelect()
        {
            Console.Clear();
            IndicateProcess.Indicator(player, mon);
            bsel.Action();
            /*
            ISelectable temp = null;
            BattleAttackTB atk = bsel.GetATK();
            BattleSkillTB skil = bsel.GetSKil();

            while (true)
            {
                temp = bsel.bSelP.SelectReturn();
                if (temp != null)
                {
                    temp.Use();
                    break;
                }

                if (temp == atk)
                {
                    temp.Use();
                    break;
                }
            }
            */
            
        }

        //Will be Deleted
        public void SkillSelect()
        {
            int select = 0;
            List<Skill> skills = player.GetSkills();
            if (skills.Count <= 0)
            {
                Console.WriteLine("Have No Skills");
                Console.ReadLine();
                BattleSelect();
            }
            else
            {
                for (int i = 0; i < skills.Count; i++)
                    Console.WriteLine($"{i + 1}.{skills[i].Name}");
                select = InputProcessor.Input(skills.Count);

                if (skills[select - 1].CanUse())
                    player.Use(skills[select - 1]);
                else
                {
                    Console.WriteLine("Not Enough MP");
                    Console.ReadLine();
                    BattleSelect();
                }
            }
        }
    }
}
