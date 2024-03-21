
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
            goldPK = 20; expPK =10;
            MaxHP = 30;
            CurHP = MaxHP;
            Atk = 5;
            Def = 0;
            Name = "Goblin";
        }
    }
    class Wolf : Monster
    {
        public Wolf()
        {
            goldPK = 40;
            expPK = 20;
            MaxHP = 60;
            Init();
            Atk = 15;
            Def = 2;
            Name = "Wolf";
        }
    }
    class Ogre : Monster
    {
        public Ogre()
        {
            goldPK = 100;
            expPK = 40;
            MaxHP = 150;
            Init();
            Atk = 25;
            Def = 5;
            Name = "Ogre";
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
