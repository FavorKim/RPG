using Usable;
using Managers;
using Processors;
using Equipments;
using System.Numerics;

namespace Entities
{

    class Player : Entity
    {


        public delegate void LevelUp();
        public event LevelUp OnLevelUp;

        public int CurMP { get; set; }
        public int MaxMP { get; protected set; }
        public int LV { get; set; }
        public int curEXP { get; set; }
        public int maxEXP { get; set; }
        public int Gold { get; set; }
        public bool IsLost { get;  set; }
        public bool IsRaged {  get; set; }

        List<Item> inventory;
        List<Skill> skills;
        SkillManager sM;
        public EquipManager eM;
        public List<Equip> Equipped;
        public List<Equip> EInven;
        public List<Item> Inventory { get { return inventory; } }
        public List<Skill> GetSkills() { return skills; }

        public Player()
        {
            MaxHP = 50;
            CurHP = 50;
            Atk = 5;
            Def = 0;
            CurMP = 20;
            MaxMP = 20;
            LV = 1;
            curEXP = 0;
            Gold = 200;
            maxEXP = 50;
            Name = "Player";
            inventory = new List<Item>();


            OnLevelUp += LevelUpStat;
            sM = new SkillManager(this);
            skills = sM.GetSkillsUsable();
        }


        public void SetEM(EquipManager eM)
        {
            this.eM = eM; 
            EInven = eM.GetEinven();
            Equipped = eM.GetEquipped();
        }

        public SkillManager GetSkillManager() { return sM; }
        public void LevelUpStat()
        {
            MaxHP += 10;
            MaxMP += 5;
            Def++;
            Atk += 5;

            FullRecover();
            Console.Clear();
            Console.WriteLine("***************Level Up!*****************");
            Console.WriteLine($"*\tHP increases about {10}!\t\t*");
            Console.WriteLine($"*\tMP increases about {5}!\t\t*");
            Console.WriteLine($"*\tAtk increases about {5}!\t\t*");
            Console.WriteLine($"*\tDef increases about 1!\t\t*");
            Console.WriteLine("*\tHP & MP Fully Recovered!\t*");
            Console.WriteLine("*****************************************");
            Console.WriteLine($"\t   Player's Level : {LV}");
            Console.ReadLine();
        }

        public void CheckLevelUp()
        {
            while (curEXP >= maxEXP)
            {
                curEXP -= maxEXP;
                maxEXP += maxEXP / 2;
                LV++;
                OnLevelUp();
            }
        }

        public void FullRecover()
        {
            CurHP = MaxHP;
            CurMP = MaxMP;
        }

        public void Lose()
        {
            curEXP -= curEXP / 10;
            Gold -= LV * 100;
            FullRecover();
            IsLost = true;

            Console.Write($"You Lost{LV * 100}Gold..!");
            Console.ReadLine();
            Console.Write($"You Lost{curEXP / 10}EXP..!");
            Console.ReadLine();
            Console.Write("Master of the INN Let You Rest");
            Console.ReadLine();
            Console.Write("You were Fully Recovered!");
            Console.ReadLine();
            Console.Write("Back to the Town");
        }

        public void Goal()
        {
            Gold += 100 + LV * 50;
            curEXP += 10 + LV * 30;
            Console.Write($"You Got{100+LV*50} Gold!");
            Console.ReadLine();
            Console.Write($"You Got {10 + LV * 30} EXP!");
            Console.ReadLine();
        }

        public void Use(IUsable item)
        {
            if (item == null) return;
            item.Use();
        }

        



    }

    
}