using Entities;
using Equipments;
using Managers;
using Managers.Selectable;

namespace Usable
{
    abstract class Item : IUsable, ISellable
    {
        public Item(Player player, ItemManager iM)
        {
            Consume = 1;
            this.player = player;
            this.iM = iM;
            onUse += iM.EmptyRemover;
            onUse += iM.SetSelected;
        }
        protected ItemManager iM;
        public string Name { get; set; }
        public int Consume { get; set; }
        public int Price { get; set; }
        public bool isItem() { return true; }
        public bool IsSelected { get; set; }
        public void ShowNum() { Console.Write($" : {Price}Gold"); }

        public abstract void Description();
        public abstract ISellable GetDeep();
        
        protected Player player;



        public void Use()
        {
            if (CanUse())
            {
                Consume--;
                Console.WriteLine("{0} Used!", Name);
                Console.ReadLine();
                Effect();
                onUse();
            }
            else
            {
                Console.WriteLine("Can't Use");
                Console.ReadLine();
            }
        }

        public delegate void OnUse();
        public event OnUse onUse;

        public bool CanUse()
        {
            if (Consume > 0)
                return true;
            else
                return false;
        }

        public abstract void Effect();
    }

    abstract class Portion : Item
    {
        Player player;
        public Portion(Player player, ItemManager iM) : base(player, iM) { this.player = player; }
        protected int grade;

        public int GetHealValue() { return grade * 20; }

        public override void Effect()
        {
            player.CurHP += 20 * grade;
            if (player.CurHP > player.MaxHP)
                player.CurHP = player.MaxHP;
        }
        public override void Description()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Name} Recover {grade * 20} HP ");
            Console.ResetColor();
        }
        

    }
    class LowPortion : Portion
    {
        
        public LowPortion(Player player, ItemManager iM) : base(player, iM)
        {
            grade = 1;
            Name = "LowPortion";
            Price = 30;
        }
        public override LowPortion GetDeep()
        {
            LowPortion deep = new LowPortion(player, iM);
            return deep;
        }
    }
    class NormalPortion : Portion 
    {
        public NormalPortion(Player player, ItemManager iM) : base(player, iM) 
        {
            grade = 2; 
            Name = "NormalPortion"; 
            Price = 50; 
        }
        public override NormalPortion GetDeep()
        {
            NormalPortion deep = new NormalPortion(player, iM);
            return deep;
        }
    }
    class HighPortion : Portion 
    {
        public HighPortion(Player player, ItemManager iM) : base(player, iM)
        {
            grade = 3; 
            Name = "HighPortion";
            Price = 70; 
        }
        public override HighPortion GetDeep()
        {
            HighPortion deep = new HighPortion(player, iM);
            return deep;
        }
    }
}
