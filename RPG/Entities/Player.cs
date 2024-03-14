using Usable;
using Manager;
using Process;

namespace Entities
{
    class Entity
    {
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
        public string Name { get; protected set; }
        public void Attack(Entity unit)
        {
            int dmg = Atk - unit.Def;
            if (dmg < 1) dmg = 1;
            unit.CurHP -= dmg;
            Console.WriteLine($"\n{Name}의 {unit.Name} 공격!");
        }

        public void Defense()
        {
            Console.WriteLine($"{Name}의 방어!");
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
        public List<Item> Inventory { get { return inventory; } }
        public List<Skill> GetSkills() { return skills; }

        public Player()
        {
            MaxHP = 100;
            CurHP = 100;
            Atk = 30;
            Def = 1;
            CurMP = 50;
            LV = 1;
            curEXP = 0;
            Gold = 200;
            maxEXP = 50;
            Name = "플레이어";
            inventory = new List<Item>();
            OnLevelUp += LevelUpStat;
            sM = new SkillManager(this);
            skills = sM.GetRTL();
        }

        public void LevelUpStat()
        {
            MaxHP += LV * 10;
            MaxMP += LV * 5;
            Def++;
            Atk += LV * 3;

            CurHP = MaxHP;
            CurMP = MaxMP;

            Console.WriteLine("레벨 업!");
            Console.WriteLine($"HP가 {LV * 10} 만큼 올랐다!");
            Console.WriteLine($"MP가 {LV * 5} 만큼 올랐다!");
            Console.WriteLine($"Atk가 {LV * 3} 만큼 올랐다!");
            Console.WriteLine($"Def가 1 만큼 올랐다!");
            Console.WriteLine("HP와 MP가 모두 회복되었다!");
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
            if (item == null) { Console.WriteLine("사용할 수 없습니다."); return; }
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