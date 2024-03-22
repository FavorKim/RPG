using Entities;
using Equipments;
using Managers;
using Managers.Selectable;
using Mapper;
using Processors;
using RPG.Select.Selectors;
using Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Usable;

namespace Selectable
{
    abstract class TextBox : ISelectable
    {
        public bool IsSelected { get; set; }
        public abstract void Use();
        public string Name { get; set; }
        public void ShowNum() { Console.WriteLine(""); }
        public bool Yes()
        {
            Console.WriteLine("Yes : Enter \t No : ESC");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                    return true;
                else if (key.Key == ConsoleKey.Escape)
                    return false;
                else continue;
            }
        }
    }
    class YesNO : TextBox
    {
        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
    class InnRestTB : TextBox
    {
        Inn inn;
        public InnRestTB(Inn inn)
        {
            this.inn = inn;
            Name = "Rest";
        }
        public override void Use()
        {
            Cleaner.CleanBox();
            Console.WriteLine("Rest for 100Gold! Wanna Rest?");
            if (Yes())
                inn.Rest();

        }
    }
}

