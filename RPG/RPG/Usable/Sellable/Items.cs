using Entities;
using Equipments;

namespace Usable
{
    abstract class Item : IUsable, ISellable
    {
        public Item() { Consume = 1; }
        public string Name { get; set; }
        public int Consume { get; set; }
        public int Price { get; set; }
        public bool isItem() { return true; }

        public bool Use(Entity unit) 
        {
            if (CanUse())
            {
                Consume--;
                Console.WriteLine("{0} Used!", Name);
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
    class LowPortion : Portion { public LowPortion() { grade = 1; Name = "LowPortion"; Price = 30; } }
    class NormalPortion : Portion { public NormalPortion() { grade = 2; Name = "NormalPortion"; Price = 50; } }
    class HighPortion : Portion { public HighPortion() { grade = 3; Name = "HighPortion"; Price = 70; } }
}
