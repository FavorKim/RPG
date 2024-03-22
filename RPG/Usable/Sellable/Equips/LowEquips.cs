using Entities;

namespace Equipments
{
    class LowPlate : Equip
    {
        
        public LowPlate(Player player) : base(player)
        {
            part = Parts.Plate;
            Value = 2;
            Price = 300;
            equipLV = 3;
            isEquipped = false;
            Name = "LowPlate";
        }
    }
    class LowHelm : Equip
    {
        public LowHelm(Player player) : base(player)
        {
            part = Parts.Head;
            Value = 1;
            Price = 150;
            equipLV = 2;
            isEquipped = false;
            Name = "LowHelm";
        }
    }
    class LowPants : Equip
    {
        public LowPants(Player player) : base(player)
        {
            part = Parts.Pants;
            Value = 2;
            Price = 200;
            equipLV = 3;
            isEquipped = false;
            Name = "LowPants";
        }
    }
    class LowBoots : Equip
    {
        public LowBoots(Player player) : base(player)
        {
            part = Parts.Feet;
            Value = 1;
            Price = 150;
            equipLV = 1;
            isEquipped = false;
            Name = "LowBoots";
        }
    }
    class OldBastard : Equip
    {
        public OldBastard(Player player) : base(player)
        {
            part = Parts.Weapon;
            Value = 4;
            Price = 150;
            equipLV = 3;
            isEquipped = false;
            Name = "OldBastard";
        }
    }
}
