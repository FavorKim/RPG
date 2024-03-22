using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    class Buffer<T>
    {
        T[] buffer;
        T[,] buffer2;
        T[] org;
        T[,] org2;
        List<T> orgList;

        Action print;
        int width;
        int height;


        public Buffer(int size, Action print, T[] org)
        {
            buffer = new T[size];
            this.print = print;
            this.org = org;
            DrawBuffer(org);
        }
        public Buffer(int width, int height, Action print, T[,] org2)
        {
            buffer2 = new T[height, width];
            this.width = width;
            this.height = height;
            this.print = print;
            this.org2 = org2;
            DrawBuffer(org2);
        }
        public Buffer(List<T> org, Action print)
        {
            this.org = new T[org.Count];
            org.CopyTo(this.org);
            this.print = print;
            buffer = new T[org.Count];
            DrawBuffer(this.org);
        }

        public T[] GetArray() { return buffer; }
        public T[,] GetArray2() { return buffer2; }


        void DrawBuffer(T[] org) { org.CopyTo(buffer, 0); }
        void DrawBuffer(T[,] org)
        {
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    buffer2[i, k] = org[i, k];
                }
            }
        }

        public void Show()
        {
            DrawBuffer(org);
            Console.SetCursorPosition(0, 0);
            print();
        }
        public void Show2()
        {
            DrawBuffer(org2);
            Console.SetCursorPosition(0, 0);
            print();
        }
        public void ShowBox()
        {
            DrawBuffer(org);
            Console.SetCursorPosition(0, 14);
            print();
        }



    }
}
