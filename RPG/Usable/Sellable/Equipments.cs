using Entities;
using Process;
namespace Equipments
{

    public interface ISellable
    {
        int Price { get; set; }
        string Name { get; set; }
        bool isItem();
    }

    public enum Parts
    {
        Weapon, Head, Plate, Pants, Feet, Max
    }

    class Equip : ISellable
    {
        public Parts part { get; set; }
        public int Value { get; protected set; }
        public int Price { get; set; }
        public int equipLV {  get; protected set; }
        public bool isEquipped { get; set; }
        public string Name { get; set; }
        public bool isItem() { return false; }

        public Equip()
        {
            isEquipped = false;
        }
        /*
        public Equip(Parts part, int Value, int Price, int equipLV,string Name)
        {
            this.part = part;
            this.Value = Value;
            this.Price = Price;
            this.equipLV = equipLV;
            this.Name = Name;
            isEquipped = false;
        }*/
    }
    class LowPlate : Equip
    {
        public LowPlate()
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
        public LowHelm()
        {
            part = Parts.Head;
            Value = 1;
            Price = 150;
            equipLV = 2;
            isEquipped = false;
            Name = "LowHelm";
        }
    }
}
