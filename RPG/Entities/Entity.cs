using Processors;
namespace Entities
{
    class Entity
    {
        public int CurHP { get; set; }
        public int MaxHP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string Name { get; protected set; }
        public void Attack(Entity unit)
        {
            int dmg = Atk - unit.Def;
            if (dmg < 1) dmg = 1;
            unit.CurHP -= dmg;
            Cleaner.CleanBox();
            Console.WriteLine($"\n{Name}'s Attack On {unit.Name}!");
            Console.ReadLine();
            Console.WriteLine($"{unit.Name} was Damaged {Atk}");
            Console.ReadLine();
            

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
}
