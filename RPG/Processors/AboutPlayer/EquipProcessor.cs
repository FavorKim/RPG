using Equipments;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    class EquipProcessor
    {
        public EquipProcessor(EquipManager eM)
        {
            this.eM = eM;
        }

        public void OpenEinven()
        {
            EquipSelect();
        }

        //Will be Deleted
        void ShowEinven()
        {
            eM.ShowEquipped();
            eM.EinvenIndicator();
        }

        //Will be Deleted
        void EquipSelect()
        {

            int num = 1;
            while (num != 0)
            {
                Console.Clear();
                ShowEinven();

                if (eM.GetEinven().Count <= 0)
                {
                    Console.WriteLine("You Do Not Have EquipManager!");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Choose Equipment You want To EquipManager\t\t 0. Exit");
                num = InputProcessor.Input(eM.GetEinven().Count);
                if (num == 0) { return; }
                Equipments.Equip equip = eM.GetEinven()[num - 1];

                eM.Equips(equip);

            }
        }

        EquipManager eM;

    }
}
