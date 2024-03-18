using Usable;
using Manager;
using Process;
using Equipments;

namespace Entities
{
    class Entity
    {
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int Atk { get; set; }
        public int Def { get;  set; }
        public string Name { get; protected set; }
        public void Attack(Entity unit)
        {
            int dmg = Atk - unit.Def;
            if (dmg < 1) dmg = 1;
            unit.CurHP -= dmg;
            Console.WriteLine($"\n{Name}'s Attack On {unit.Name}!");
        }

        public void Defense()
        {
            Console.WriteLine($"{Name}'s Defense!");
            CurHP += 2 * Def;
        }

        public bool IsDead()
        {
            if (CurHP > 0) return false;
            else return true;
        }

    }


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

        List<Item> inventory;
        List<Skill> skills;
        SkillManager sM;
        Equip[] Einven;
        public List<Item> Inventory { get { return inventory; } }
        public List<Skill> GetSkills() { return skills; }

        public Player()
        {
            MaxHP = 100;
            CurHP = 100;
            Atk = 10;
            Def = 1;
            CurMP = 50;
            LV = 1;
            curEXP = 0;
            Gold = 200;
            maxEXP = 50;
            Name = "Player";
            inventory = new List<Item>();
            OnLevelUp += LevelUpStat;
            sM = new SkillManager(this);
            skills = sM.GetSkillsUsable();
            Einven = new EquipManager(this).GetEquipped();
        }

        public void LevelUpStat()
        {
            MaxHP += LV * 10;
            MaxMP += LV * 5;
            Def++;
            Atk += LV * 3;

            CurHP = MaxHP;
            CurMP = MaxMP;
            Console.Clear();
            Console.WriteLine("***************Level Up!*****************");
            Console.WriteLine($"*\tHP increases about {LV * 10}!\t\t*");
            Console.WriteLine($"*\tMP increases about {LV * 5}!\t\t*");
            Console.WriteLine($"*\tAtk increases about {LV * 3}!\t\t*");
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





        public void Use(IUsable item, Entity dest)
        {
            if (item == null) { Console.WriteLine("Can't Use"); return; }
            item.Use(dest);
        }



    }

    /*
     플레이어의 상태
    돈, 공격력, 체력, 방어력, 레벨, 경험치.

    플레이어가 가진 것.
    장비(무기, 갑옷, 방패), 아이템, 스킬,

    플레이어의 기능
    이동, 전투, 스킬사용, 아이템사용, 구매, 판매?, 레벨업, 
     
     
     
     */
}