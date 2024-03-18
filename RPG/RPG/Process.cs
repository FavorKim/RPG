using Entities;
using Usable;
using Manager;
using Shop;
using Equipments;
using System;

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
            eM = new EquipManager(player);
            sP = new ShopProcessor(player, iM, eM, this);
            eP = new EquipProcessor(eM);
        }


        public void MainProcess()
        {
            iM.AddInven(new LowPortion());
            player.Gold += 99999;
            player.LV += 1;
            sP.Shop();
            eP.OpenEinven();
            

        }

        public static void Indicator(Player player, Monster mon)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌──────────────────────────────────────────────────┐");
            Console.Write($"│{player.Name}'s HP : {player.CurHP}\t\t\t\t   │");
            if (player.CurHP < 100) Console.Write(" \n"); else Console.Write('\n');
            Console.WriteLine($"│{player.Name}'s MP : {player.CurMP}\t\t{mon.Name}'s HP : {mon.CurHP}   │");
            Console.WriteLine($"│{player.Name}'s Atk : {player.Atk}     VS \t{mon.Name}'s Atk : {mon.Atk}   │");
            Console.WriteLine($"│{player.Name}'s Def : {player.Def}   \t\t{mon.Name}'s Def : {mon.Def}   │");
            Console.WriteLine($"│{player.Name}'s EXP : {player.curEXP}/{player.maxEXP}\t\t\t\t   │");
            Console.WriteLine($"│{player.Name}'s LV : {player.LV}                                   │");
            Console.WriteLine("└──────────────────────────────────────────────────┘\n");
            Console.ResetColor();
        }
        public void Indicator()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌───────────────────────────────┐");
            Console.Write($"│\t{player.Name}'s HP : {player.CurHP}\t│");
            if (player.CurHP < 100) Console.Write(" \n"); else Console.Write('\n');
            Console.WriteLine($"│\t{player.Name}'s MP : {player.CurMP}\t│");
            Console.WriteLine($"│\t{player.Name}'s Atk : {player.Atk}\t│");
            Console.WriteLine($"│\t{player.Name}'s Def : {player.Def}\t│");
            Console.WriteLine($"│\t{player.Name}'s EXP : {player.curEXP}/{player.maxEXP}\t│");
            Console.WriteLine($"│\t{player.Name}'s LV : {player.LV}\t\t│");
            Console.WriteLine("└───────────────────────────────┘\n");
            Console.ResetColor();
            eM.EinvenIndicator();
            iM.InvenIndicator();

            
        }

        public void ItemSelect()
        {
            iM.EmptyRemover();

            Console.WriteLine();

            if (iM.inventory.Count == 0)
            {
                Console.WriteLine("Inventory is Empty");
                Console.ResetColor();
                bp.BattleSelect();
            }
            else
            {
                for (int i = 0; i < iM.inventory.Count; i++)
                    Console.WriteLine($"{i + 1}. {iM.inventory[i].Name} : {iM.inventory[i].Consume}ea");

                Console.WriteLine("0. Prev");
                int num = InputProcess.Input(iM.inventory.Count);
                if (num == 0)
                {
                    bp.BattleSelect();
                }
                else
                    player.Use(iM.inventory[num - 1], player);
            }
        }



        Player player;
        MonsterManager monM;
        ItemManager iM;
        BattleProcessor bp;
        ShopProcessor sP;
        EquipManager eM;
        EquipProcessor eP;
    }

    class InputProcess
    {
        public static int Input(int length)
        {
            int input = length + 1;
            while (input < 0 || input > length)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Wrong Input. TryAgain");
                }
            }
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


        public BattleProcessor(Player player, MonsterManager monM, ItemManager iM, MainProcessor mainP)
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
                Console.WriteLine("WIN!");
                Console.ReadLine();
                player.curEXP += mon.ExpPK;
                player.Gold += mon.GoldPK;
                OnBattleOver();
            }
            else
            {
                Console.ReadLine();
                Console.WriteLine("Lose...");
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
                    Console.ReadLine();
                }
            }
            BattleResult();
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
            MainProcessor.Indicator(player, mon);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1.Attack 2. Skills 3. Inventory : ");
            Console.ResetColor();
            int num;
            num = InputProcess.Input(3);

            switch (num)
            {
                case 0:
                    BattleSelect();
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    player.Attack(mon);
                    Console.ResetColor();
                    break;

                case 2:
                    SkillSelect();
                    break;

                case 3:
                    mainP.ItemSelect();
                    break;

                default:
                    Console.WriteLine("\nWrong Input.");
                    Console.ReadLine();
                    break;
            }
        }

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
                    Console.WriteLine($"{i + 1}.{skills[i].name}");
                select = InputProcess.Input(skills.Count);

                if (skills[select - 1].CanUse())
                    player.Use(skills[select - 1], player);
                else
                {
                    Console.WriteLine("Not Enough MP");
                    Console.ReadLine();
                    BattleSelect();
                }
            }
        }
    }

    class ShopProcessor
    {
        Merchant shop;
        Player player;
        ItemManager iM;
        EquipManager eM;
        MainProcessor mP;

        public ShopProcessor( Player player ,ItemManager iM, EquipManager eM, MainProcessor mP)
        {
            shop = new Merchant(iM, eM);
            this.player = player;
            this.iM = iM;
            this.eM = eM;
            this.mP = mP;
        }



        public void Shop()
        {
            ShopSelect();
        }

        void ShopSelect()
        {
            int num = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"1. Buy 2. Sell 3. Status 0. Exit \t\t\t Remaining Gold : {player.Gold}");
                num = InputProcess.Input(3);
                switch (num)
                {
                    case 0:
                        return;
                    case 1:
                        Buy();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1. Sell Item         2. Sell Equipments\t\t 0. Exit");
                        Sell(InputProcess.Input(2));
                        break;
                    case 3:
                        Console.Clear();
                        mP.Indicator();
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Wrong Input. TryAgain");
                        ShopSelect();
                        break;
                }
            }
        }

        void Buy()
        {
            Console.Clear();
            shop.ShowList();
            Console.WriteLine($"\nRemaining Gold : {player.Gold}g \n0. Exit");
            int num = InputProcess.Input(shop.GetSellables().Count);
            if(num == 0) { return; }

            ISellable sellable = shop.GetSellable(num - 1);
            Console.Clear();
            shop.ShowList();
            Console.WriteLine();
            Console.WriteLine($"{sellable.Name}'s Price is {sellable.Price}gold. is That OK? \n 1. OK 2. No");
            
            num = InputProcess.Input(2);
            
            if (num == 1)
            {
                Console.Clear();
                shop.ShowList();
                Console.WriteLine();
                shop.Purchase(player, sellable);
                Console.WriteLine($"You Got {sellable.Name}! ");
                Console.WriteLine($"Remaining Gold : {player.Gold}");
                Console.ReadLine();
            }
            Buy();
        }
        void Sell(int num)
        {
            Console.Clear();
            if (num == 1)
            {
                if(iM.inventory.Count <= 0)
                {
                    Console.WriteLine("You Do Not Have Items!");
                    Console.ReadLine();
                    return;
                }

                iM.InvenIndicator();
                Console.WriteLine("\nSelect Item You want to Sell\t\t 0. Exit");


                num = InputProcess.Input(iM.inventory.Count);
                if (num == 0) { return; }

                Console.WriteLine($"{iM.inventory[num - 1].Name}'s Sell Price is " +
                    $"{iM.inventory[num - 1].Price / 5}gold. is that OK? \n 1. OK 2. NO");

                num = InputProcess.Input(2);

                if (num == 1)
                    shop.Sell(player, iM.inventory[num - 1]);
                else
                    Sell(1);
            }
            else if (num == 2)
            {
                if (eM.GetEinven().Count <= 0)
                {
                    Console.WriteLine("You Do Not Have Equipments!");
                    Console.ReadLine();
                    return;
                }
                eM.EinvenIndicator();
                Console.WriteLine("Select Equipments You want to Sell\t\t 0. Exit");
                num = InputProcess.Input(iM.inventory.Count);

                if (num == 0) { return; }

                shop.Sell(player, eM.GetEinven()[num - 1]);
            }
            else
                return;
        }
        /*
        void BuyItem()
        {
            shop.ShowItemList();
            int num = InputProcess.Input(shop.GetItemList().Count);
            Item item = shop.GetItem(num - 1);

            shop.ItemPurchase(player, item);
        }
        void BuyEquip()
        {
            shop.ShowEquipList();
            int num = InputProcess.Input(shop.GetEquipList().Count);
            Equip equip = shop.GetEquip(num - 1);

            shop.EquipPurchase(player, equip);
        }
        */
    }

    class EquipProcessor
    {
        public EquipProcessor(EquipManager eM)
        {
            this.eM = eM;
        }

        public void OpenEinven()
        {
            EquipSelect();
        }
        void ShowEinven()
        {
            eM.ShowEquipped();
            eM.EinvenIndicator();
        }

        void EquipSelect()
        {
            
            int num = 1;
            while (num != 0)
            {
                Console.Clear();
                ShowEinven();

                if (eM.GetEinven().Count <= 0)
                {
                    Console.WriteLine("You Do Not Have Equipments!");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Choose Equipment You want To Equip\t\t 0. Exit");
                num = InputProcess.Input(eM.GetEinven().Count);
                if (num == 0) { return; }
                Equip equip = eM.GetEinven()[num - 1];

                eM.Equips(equip);
                
            }
        }

        EquipManager eM;

    }
}