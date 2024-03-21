using CMaze;
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
            itemM = new ItemManager(player.Inventory);
            equipM = player.eM;
            skillM = player.GetSkillManager();
            shopP = new ShopProcessor(player, itemM, equipM, this);
            indicateP = new IndicateProcess(player);
            iStat = new MenuSelector(skillM, itemM, indicateP, equipM);
            mapP = new MapProcessor(itemM, equipM, indicateP, iStat);
            maze = new Maze();
            innP = new Inn(player);
            shopSel = new ShopSelector(player, shopP, itemM, equipM);
            inSel = new InnSelector(innP, player);
            bsel = new BattleSelector(player, skillM, itemM);
            battleP = new BattleProcessor(player, monM, itemM, this, skillM,bsel);
            dunP = new DungeonProcessor(maze, battleP, player,iStat);
        }


        public void MainProcess()
        {
            player.Gold += 9999;
            Entering en;
            en = mapP.Action();
            Enter(en);
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
        ItemManager itemM;
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
