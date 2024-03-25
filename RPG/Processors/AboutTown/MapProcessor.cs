using Managers;
using Mapper;
using Select;
namespace Processors
{
    class MapProcessor
    {
        Map mP;
        ItemManager iM;
        EquipManager eM;
        IndicateProcess iP;
        MenuSelector iStat;
        public MapProcessor(ItemManager iM, EquipManager eM, IndicateProcess iP, MenuSelector iStat)
        {
            this.iM = iM;
            this.eM = eM;
            this.iP = iP;
            this.iStat = iStat;
            mP = new Map(iM, eM, iP, iStat);

        }

        public Entering Action()
        {
            Entering en = Entering.Fail;
            Cleaner.Clear();
            while (en == Entering.Fail)
            {
                mP.MBuffer.Show2();
                en = mP.Move();
            }

            return en;
        }


    }
}
