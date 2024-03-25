using Managers.Selectable;

namespace Processors
{
    enum Choose
    {
        NEXT, PREV, SELECT, EXIT, SPACE, NONE
    }
    class SelectProcessor<T> where T : ISelectable
    {
        List<T> list;
        Buffer<T> buffer;
        List<T> prev;
        List<T> next;

        public bool ListisEmpty() { return list.Count == 0; }

        public SelectProcessor(List<T> list)
        {
            this.list = list;
            buffer = new Buffer<T>(list, Show);
        }


        public Buffer<T> GetBuffer() { return buffer; }

        public ISelectable SelectReturn()
        {
            ISelectable item = null;

            if (list == null) return null;

            if (list.Count <= 0)
            {
                Cleaner.CleanBox();
                Console.WriteLine("\n\n Empty");
                Console.ReadLine();
                return null;
            }

            SetList();

            Cleaner.CleanBox();
            buffer.ShowBox();
            Choose c = Choose.PREV;

            while (c != Choose.SELECT || c != Choose.EXIT)
            {
                c = GetChoose();
                Choice(c);
                buffer.ShowBox();

                if (list.Count <= 0)
                {
                    Cleaner.CleanBox();
                    Console.WriteLine("\n\n Empty");
                    Console.ReadLine();
                    return null;
                }

                if (c == Choose.EXIT)
                    break;
                if (c == Choose.SELECT)
                    break;
            }
            if (c == Choose.SELECT)
            {
                item = GetSelected();
                Cleaner.CleanBox();
                SetList();
            }

            if (c == Choose.EXIT)
            {
                SetList();
                Cleaner.CleanBox();
                return null;
            }

            if (item != null)
            {
                SetList();
                Cleaner.CleanBox();
                return item;
            }
            else
            {
                SetList();
                Cleaner.CleanBox();
                return null;
            }

        }
        public void SelectVoid()
        {
            if (list == null) return;

            if (list.Count <= 0)
            {
                Cleaner.CleanBox();
                Console.WriteLine("\n\n Empty(select void)");
                Console.ReadLine();
                return;
            }
            SetList();

            Cleaner.CleanBox();
            buffer.ShowBox();


            Choose c = Choose.PREV;

            while (c != Choose.EXIT)
            {
                if (list.Count <= 0)
                {
                    Cleaner.CleanBox();
                    Console.WriteLine("\n\n Empty(select void)");
                    Console.ReadLine();
                    break;
                }
                buffer.ShowBox();
                c = GetChoose();
                Choice(c);
                buffer.ShowBox();
                if (list.Count <= 0)
                {
                    Cleaner.CleanBox();
                    Console.WriteLine("\n\n Empty (select void in roop)");
                    Console.ReadLine();
                    break;
                }
                if (c == Choose.EXIT)
                    break;
                if (c == Choose.SELECT)
                {
                    GetSelected().Use();
                    SetList();
                    Cleaner.CleanBox();
                }

            }
            SetList();
            Cleaner.CleanBox();
        }

        public void Show()
        {
            Cleaner.CleanBox();
            if (list.Count == 0)
            {
                Cleaner.CleanBox();
                Console.WriteLine("Empty(show)");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].IsSelected)
                {
                    Console.Write($"{i + 1}. {list[i].Name} "); list[i].ShowNum();

                }
                else if (list[i].IsSelected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" ▶ ");
                    Console.Write($"{i + 1}. {list[i].Name} "); list[i].ShowNum();
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Prev (ESC)");
        }


        ISelectable GetSelected()
        {
            ISelectable selected = null;
            foreach (ISelectable s in list)
            {
                if (!s.IsSelected)
                    continue;
                selected = s;
            }
            if (selected != null)
                return selected;
            else
            {
                Console.WriteLine("GetSelected() Fail at SelectProcessor");
                Console.ReadLine();
                return null;
            }
        }
        int GetSelectedIndex()
        {
            int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].IsSelected)
                    continue;
                index = i;
            }
            return index;
        }

        void Choice(Choose c)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("getChoiceEmpty");
                c = Choose.EXIT;
                return;
            }

            ISelectable cursor = null;
            int curIndex = 0;

            cursor = GetSelected();
            curIndex = GetSelectedIndex();

            if (c == Choose.NEXT)
            {
                if (curIndex < list.Count - 1)
                {
                    cursor.IsSelected = false;
                    list[curIndex + 1].IsSelected = true;
                }

            }
            else if (c == Choose.PREV)
            {
                if (curIndex > 0)
                {
                    cursor.IsSelected = false;
                    list[curIndex - 1].IsSelected = true;
                }
            }
        }

        Choose GetChoose()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.UpArrow:
                    return Choose.PREV;

                case ConsoleKey.RightArrow:
                case ConsoleKey.DownArrow:
                    return Choose.NEXT;

                case ConsoleKey.Enter:
                    return Choose.SELECT;

                case ConsoleKey.Escape:
                    return Choose.EXIT;


                default:
                    return Choose.NONE;
            }
        }

        void SetList()
        {
            if (list.Count <= 0 || list == null)
                return;

            foreach (var item in list)
                item.IsSelected = false;
            list[0].IsSelected = true;
        }



    }
}
