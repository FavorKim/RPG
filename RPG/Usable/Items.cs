using Entities;

namespace Usable
{
    abstract class Item : IUsable
    {
        public Item() { Consume = 1; }
        protected string name;
        public string GetName() { return name; }
        public int Consume { get; set; }
        public int Price { get; protected set; }


        public bool Use(Entity unit) 
        {
            if (CanUse())
            {
                Consume--;
                Console.WriteLine("{0} 사용!", name);
                Effect(unit);
                return true;
            }
            else { return false; }
        }
        public bool CanUse()
        {
            if (Consume > 0)
                return true;
            else
                return false;
        }

        public abstract void Effect(Entity unit);
    }

    class Portion : Item
    {
        protected int grade;
        public override void Effect(Entity unit)
        {
            unit.CurHP += 20 * grade;
            if (unit.CurHP > unit.MaxHP)
                unit.CurHP = unit.MaxHP;
        }
    }
    class LowPortion : Portion { public LowPortion() { grade = 1; name = "LowPortion"; Price = 30; } }
    class NormalPortion : Portion { public NormalPortion() { grade = 2; name = "NormalPortion"; Price = 50; } }
    class HighPortion : Portion { public HighPortion() { grade = 3; name = "HighPortion"; Price = 70; } }
}
