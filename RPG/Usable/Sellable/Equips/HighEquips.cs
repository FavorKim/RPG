using Entities;

namespace Equipments
{
    class HighPlate : Equip
    {
        public HighPlate(Player player) : base(player)
        {
            part = Parts.Plate;
            Value = 3;
            Price = 500;
            equipLV = 6;
            isEquipped = false;
            Name = "HighPlate";
        }
    }
    class HighHelm : Equip
    {
        public HighHelm(Player player) : base(player)
        {
            part = Parts.Head;
            Value = 2;
            Price = 300;
            equipLV = 5;
            isEquipped = false;
            Name = "HighHelm";
        }
    }
    class HighPants : Equip
    {
        public HighPants(Player player) : base(player)
        {
            part = Parts.Pants;
            Value = 3;
            Price = 400;
            equipLV = 6;
            isEquipped = false;
            Name = "HighPants";
        }
    }
    class HighBoots : Equip
    {
        public HighBoots(Player player) : base(player)
        {
            part = Parts.Feet;
            Value = 2;
            Price = 300;
            equipLV = 5;
            isEquipped = false;
            Name = "HighBoots";
        }
    }
    class TweiHander : Equip
    {
        public TweiHander(Player player) : base(player)
        {
            part = Parts.Weapon;
            Value = 15;
            Price = 2000;
            equipLV = 12;
            isEquipped = false;
            Name = "TweiHander";
        }
    }
}
