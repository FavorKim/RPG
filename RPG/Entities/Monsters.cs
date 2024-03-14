
namespace Entities
{

    class Monster : Entity
    {
        protected int goldPK;
        protected int expPK;
        public int GoldPK { get { return goldPK; } protected set { goldPK = value; } }
        public int ExpPK { get { return expPK; } protected set { expPK = value; } }

        public void Init()
        {
            if (CurHP != MaxHP)
                CurHP = MaxHP;
        }
    }

    class Goblin : Monster
    {
        public Goblin()
        {
            goldPK = 5; expPK = 500;
            MaxHP = 30;
            CurHP = MaxHP;
            Atk = 5;
            Def = 0;
            Name = "Goblin";
        }
    }

    class Dummy : Monster
    {
        public Dummy()
        {
            MaxHP = 9999;
            CurHP = 9999;
            Atk = 0;
            Def = 0;
            Name = "Dummy";
        }
    }

}
