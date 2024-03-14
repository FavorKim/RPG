using Entities;
using Usable;
using Manager;
using System.Security.Cryptography.X509Certificates;

namespace Process
{
    class MainProcessor
    {

        public MainProcessor()
        {
            player = new Player();
            monM = new MonsterManager();
            monM.AddMon(new Goblin());
            monM.AddMon(new Dummy());
            iM = new ItemManager(player.Inventory);
            bp = new BattleProcessor(player, monM, iM, this);
        }


        public void MainProcess()
        {
            iM.GetItem(new LowPortion());
            bp.Battle("Goblin");
            bp.Battle("Goblin");
        }

        public static void Indicator(Player player, Monster mon)
        {
            Console.WriteLine("=========================================================================");
            Console.WriteLine($"{player.Name}의 체력 : {player.CurHP}\t\t\t\t\t\t\t");
            Console.WriteLine($"{player.Name}의 MP : {player.CurMP}\t\t\t\t\t\t\t");
            Console.WriteLine($"{player.Name}의 공격력 : {player.Atk} \tVS\t {mon.Name}의 체력 : {mon.CurHP}  ");
            Console.WriteLine($"{player.Name}의 방어력 : {player.Def} \t\t\t\t\t\t\t");
            Console.WriteLine($"{player.Name}의 경험치 : {player.curEXP}/{player.maxEXP}\t\t\t\t\t\t");
            Console.WriteLine($"{player.Name}의 레벨 : {player.LV}\t\t\t\t\t\t");
            Console.WriteLine("=========================================================================\n");
        }
        public static void Indicator(Player player)
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"{player.Name}의 체력   : {player.CurHP}\t");
            Console.WriteLine($"{player.Name}의 M P\t   : {player.CurMP}\t\t");
            Console.WriteLine($"{player.Name}의 공격력 : {player.Atk}\t\t");
            Console.WriteLine($"{player.Name}의 방어력 : {player.Def}\t\t");
            Console.WriteLine($"{player.Name} 의 경험치 :  {player.curEXP} / {player.maxEXP}");
            Console.WriteLine("=================================\n");
        }

        public void ItemSelect()
        {
            iM.EmptyRemover();

            Console.WriteLine();

            if (iM.inventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
                bp.BattleSelect();
            }
            else
            {
                for (int i = 0; i < iM.inventory.Count; i++)
                    Console.WriteLine($"{i + 1}. {iM.inventory[i].GetName()} : {iM.inventory[i].Consume}ea");

                int num = InputProcess.Input(iM.inventory.Count);

                player.Use(iM.inventory[num - 1], player);
            }
        }

       

        Player player;
        MonsterManager monM;
        ItemManager iM;
        BattleProcessor bp;
    }

    class InputProcess
    {
        public static int Input(int length)
        {
            int input = 0;
            while (input <= 0 || input > length)
                input = int.Parse(Console.ReadLine());
            return input;

        }

    }

    class BattleProcessor
    {
        Player player;
        Monster mon;
        MonsterManager monM;
        ItemManager iM;
        MainProcessor mainP;
        SkillManager sM;


        public delegate void BattleOver();
        public event BattleOver OnBattleOver;


        public BattleProcessor(Player player,  MonsterManager monM, ItemManager iM, MainProcessor mainP)
        {
            this.player = player;
            this.monM = monM;
            this.iM = iM;
            this.mainP = mainP;
            OnBattleOver += player.CheckLevelUp;
        }

        public void BattleResult()
        {
            if (mon.IsDead())
            {
                Console.WriteLine("전투 승리");
                player.curEXP += mon.ExpPK;
                player.Gold += mon.GoldPK;
                OnBattleOver();
            }
            else
                Console.WriteLine("전투 패배");
        }

        public void Battle(string monName)
        {
            Encounter(monM.GetMonster(monName));

            while (!mon.IsDead() && !player.IsDead())
            {
                MainProcessor.Indicator(player, mon);
                BattleSelect();
                if (!mon.IsDead())
                    MonsterTurn();
            }
            BattleResult();
        }

        public void MonsterTurn()
        {
            mon.Attack(player);
        }

        public void Encounter(Monster mon) 
        {
            this.mon = mon; 
            this.mon.Init();
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
            Console.Write("1.공격 2. 스킬 3. 아이템사용 : ");
            int num = 1;
            num = InputProcess.Input(3);

            switch (num)
            {
                case 1:
                    player.Attack(mon);
                    break;

                case 2:
                    SkillSelect();
                    break;

                case 3:
                    mainP.ItemSelect();
                    break;

                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    break;
            }
        }

        public void SkillSelect()
        {
            List<Skill> skills = player.GetSkills();
            if (skills.Count <= 0)
            {
                Console.WriteLine("스킬을 배우지 않았습니다.");
                BattleSelect();
            }
            else
            {
                for (int i = 0; i < skills.Count; i++)
                    Console.WriteLine($"{i + 1}.{skills[i].name}");
                int select = InputProcess.Input(skills.Count);
                player.Use(skills[select - 1], player);
            }
        }



        
    }


}