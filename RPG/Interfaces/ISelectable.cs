using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Selectable
{
    interface ISelectable
    {
        bool IsSelected { get; set; }
        // Entity dest { get; set; }
        string Name { get; set; }
        void Use();
    }
}
