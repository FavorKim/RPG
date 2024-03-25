

namespace Equipments
{
    public interface ISellable 
    {
        int Price { get; set; }
        string Name { get; set; }
        bool isItem();
        public ISellable GetDeep();
        
    }
}
