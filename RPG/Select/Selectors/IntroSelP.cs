using Processors;
using RPG.Select.TextBoxes;
using Selectable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Select.Selectors
{
    internal class IntroSelP
    {
        List<TextBox> textBoxes = new List<TextBox>();
        Start start;
        EXIT exit;
        public SelectProcessor<TextBox> selP;
        MainProcessor mP;
        public IntroSelP()
        {
            mP = new MainProcessor();
            Init();
        }
        void Init()
        {
            start = new Start(mP);
            exit = new EXIT();
            textBoxes = new List<TextBox>();
            textBoxes.Add(start);
            textBoxes.Add(exit);
            selP = new SelectProcessor<TextBox>(textBoxes);
        }
    }
}
