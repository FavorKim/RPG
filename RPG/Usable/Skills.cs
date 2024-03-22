using Entities;
namespace Usable
{
    

    abstract class Skill : IUsable
    {
        public Entity dest { get; set; }
        protected Player player;
        public string Name { get; set; }
        public int Consume { set; get; }
        public int SkillLV { get; protected set; }
        protected int value;
        public bool IsLearned {  set; get; }
        public bool IsAttack { get; protected set; }
        public bool IsSelected { get; set; }
        public void ShowNum() { Console.WriteLine($" : {Consume}mp"); }

        public Skill(Player player) 
        {
            this.player = player;
            IsLearned = false; 
        }
        public void Use()
        {
            if (CanUse())
            {
                player.CurMP -= Consume;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0} On {1}!", Name, dest.Name);
                Console.ResetColor();
                Effect();
            }
            else
                return;
        }
        public bool CanUse()
        {
            return player.CurMP >= Consume;
        }

        public void SetDest(Entity dest)
        {
            this.dest = dest;
        }

        public abstract void Effect();
    }

    class Slash : Skill
    {
        public Slash(Player player) :base(player)
        {
            value = player.Atk + (player.LV * 4);
            Consume = 10;
            SkillLV = 3;
            Name = "Slash";
            IsAttack = true;
        }

        void SetValue()
        {
            value = player.Atk + (player.LV * 4);
        }

        public override void Effect() 
        {
            SetValue();
            dest.CurHP -= value;
            Console.WriteLine($"{dest.Name} was Damaged {value}!");
            Console.ReadLine();
        }
    }

    class Rage : Skill
    {
        public Rage(Player player) : base(player) 
        {
            value = 10;
            Consume = 15;
            SkillLV = 5;
            Name = "Rage";
            IsAttack = false;
        }
        public override void Effect() 
        {
            
            dest.Atk += value;
            dest.MaxHP += (value*3);
            dest.CurHP += (value*3);
            player.IsRaged = true;
            Console.WriteLine($"{dest.Name}'s Current HP Increases {value*3}!");
            Console.ReadLine();
            Console.WriteLine($"{dest.Name}'s Max HP Increases {value * 3}!");
            Console.ReadLine();
            Console.WriteLine($"{dest.Name}'s Atk Increases {value}!");
            Console.ReadLine();
        }
    }

    


}
