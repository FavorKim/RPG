﻿using CMaze;
using Entities;
using Managers;
using Mapper;
using Select;
using Equipments;
using RPG.Select.Selectors;
using Selectors;

namespace Processors
{
    class MainProcessor
    {
        int debugC = 0;
        public MainProcessor()
        {
            player = new Player();
            equipM = new EquipManager();
            equipM.SetPlayer(player);
            player.SetEM(equipM);
            monM = new MonsterManager();
            monM.AddMon(new Goblin());
            monM.AddMon(new Dummy());
            monM.AddMon(new Ogre());
            monM.AddMon(new Wolf());
            playerItemM = new ItemManager(player.Inventory);
            
            equipM = player.eM;
            skillM = player.GetSkillManager();
            shopP = new ShopProcessor(player, equipM, this, playerItemM);
            indicateP = new IndicateProcess(player);
            iStat = new MenuSelector(skillM, playerItemM, indicateP, equipM,player);
            mapP = new MapProcessor(playerItemM, equipM, indicateP, iStat);
            maze = new Maze();
            innP = new Inn(player);
            shopSel = new ShopSelector(player, shopP, playerItemM, equipM);
            inSel = new InnSelector(innP, player,indicateP);
            bsel = new BattleSelector(player, skillM, playerItemM);
            battleP = new BattleProcessor(player, monM, playerItemM, this, skillM,bsel);
            dunP = new DungeonProcessor(maze, battleP, player,iStat);
        }


        public void MainProcess()
        {
            
            DebugCheck(ref debugC);

            Entering en;
            en = mapP.Action();
            Enter(en);
        }
       
        void DebugSet()
        {
            player.Gold += 9999;
            foreach(Monster m in monM.Mons.Values)
                m.SetDebugMon();
        }
        void DebugCheck(ref int check)
        {
            if (check != 0) return;
            Console.WriteLine("is DebugMode?\n 1.Yes 2.No");
            int num = int.Parse(Console.ReadLine());
            if (num == 1)
                DebugSet();
            check++;
        }
        
        void Enter(Entering en)
        {
            switch (en)
            {
                case Entering.Shop:
                    shopSel.firstSP.SelectVoid();
                    break;

                case Entering.Inn:
                    inSel.Welcome();
                    break;

                case Entering.Dungeon:
                    dunP.Action();
                    break;
                
                default:
                    break;
            }
            MainProcess();
        }

        #region var
        Player player;
        MonsterManager monM;
        ItemManager playerItemM;
        BattleProcessor battleP;
        ShopProcessor shopP;
        EquipManager equipM;
        Maze maze;
        MapProcessor mapP;
        DungeonProcessor dunP;
        Inn innP;
        IndicateProcess indicateP;
        MenuSelector iStat;
        ShopSelector shopSel;
        InnSelector inSel;
        BattleSelector bsel;
        SkillManager skillM;
        #endregion
    }
}
