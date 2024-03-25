using Entities;
using Managers;
using RPG.Select.Selectors;
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
            OnBattleOver += sM.ResetRage;
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
                Console.WriteLine($"You Got {mon.GoldPK}Gold from {mon.Name}!");
                Console.ReadLine();
                Console.WriteLine($"You Got {mon.ExpPK}EXP from {mon.Name}!");
                Console.ReadLine();
                OnBattleOver();
            }
            else
            {
                Console.WriteLine("Lose...");
                player.Lose();
                Console.ReadLine();
                OnBattleOver();
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
            IndicateProcess.BattleIndicator(player, mon);
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
            IndicateProcess.BattleIndicator(player, mon);
            bsel.Action();
        }

    }
}
