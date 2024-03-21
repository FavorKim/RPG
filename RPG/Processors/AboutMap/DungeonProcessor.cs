using CMaze;
using Entities;
using Select;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    class DungeonProcessor
    {
        Maze m;
        BattleProcessor battleP;
        Player player;
        MenuSelector ms;
        string mon;
        public DungeonProcessor(Maze m, BattleProcessor battleP, Player player, MenuSelector ms)
        {
            this.m = m;
            this.battleP = battleP;
            this.player = player;
            this.ms = ms;
        }

        public Tile Action()
        {
            Init();
            Tile tile = Tile.Fail;
            Cleaner.Clear();
            while (tile != Tile.GOAL)
            {
                m.Rbuffer.Show();
                tile = m.Move();
                if (tile == Tile.Monster)
                {
                    Encounter();
                    if (player.IsLost)
                        break;
                }
                if(tile == Tile.ESC)
                {
                    TextBox temp = (TextBox)ms.selP.SelectReturn();

                    if (temp != null)
                    {
                        temp.Use();
                        Cleaner.CleanBox();
                    }
                    
                    Cleaner.CleanBox();
                }
            }
            if (!player.IsLost)
            {
                m.OnGoal();
                player.Goal();
            }
            else
            {
                player.IsLost = false;
                m.ResetMaze();
            }
            
            return tile;
        }




        void Encounter()
        {
            battleP.Battle(mon);
        }
        void Init()
        {
            m.SetLevel(player);
            SetMon(m.GetLevel());
            m.SetMonsters();
        }

        void SetMon(int LV)
        {
            switch (LV)
            {
                case 0:
                    mon = "Goblin";
                    break;
                case 1:
                    mon = "Wolf";
                        break;
                case 2:
                    mon = "Ogre";
                    break;
                default:
                    break;
            }
        }

    }
}
