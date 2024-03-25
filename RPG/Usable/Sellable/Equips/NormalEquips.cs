using Entities;
namespace Equipments
{
    class NormalPlate : Equip
    {
        public NormalPlate(Player player) : base(player)
        {
            part = Parts.Plate;
            Value = 3;
            Price = 500;
            equipLV = 6;
            isEquipped = false;
            Name = "NormalPlate";
        }
    }
    class NormalHelm : Equip
    {
        public NormalHelm(Player player) : base(player)
        {
            part = Parts.Head;
            Value = 2;
            Price = 300;
            equipLV = 5;
            isEquipped = false;
            Name = "NormalHelm";
        }
    }
    class NormalPants : Equip
    {
        public NormalPants(Player player) : base(player)
        {
            part = Parts.Pants;
            Value = 3;
            Price = 400;
            equipLV = 6;
            isEquipped = false;
            Name = "NormalPants";
        }
    }
    class NormalBoots : Equip
    {
        public NormalBoots(Player player) : base(player)
        {
            part = Parts.Feet;
            Value = 2;
            Price = 300;
            equipLV = 5;
            isEquipped = false;
            Name = "NormalBoots";
        }
    }
    class Sabor : Equip
    {
        public Sabor(Player player) : base(player)
        {
            part = Parts.Weapon;
            Value = 10;
            Price = 800;
            equipLV = 7;
            isEquipped = false;
            Name = "Sabor";
        }
    }
}
