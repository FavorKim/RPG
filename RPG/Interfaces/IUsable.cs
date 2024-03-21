using Entities;
using Managers.Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usable
{
    interface IUsable : ISelectable
    {
        int Consume { get; set; }
        bool CanUse();
        void Use();
        abstract void Effect();
    }
}
