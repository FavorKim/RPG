using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Processors;
using Selectable;

namespace RPG.Select.TextBoxes
{
    class Start : TextBox
    {
        MainProcessor mP;
        public Start(MainProcessor mP)
        {
            Name = "Start";
            this.mP = mP;
        }
        public override void Use()
        {
            mP.MainProcess();
        }
    }

    class EXIT : TextBox
    {
        public EXIT()
        {
            Name = "EXIT";
        }
        public override void Use()
        {
            return;
        }
    }

}
