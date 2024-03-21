using Entities;
using Processors;
using Usable;
using Managers;
using System.Diagnostics;
using System.Windows;

namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainProcessor main = new MainProcessor();
            main.MainProcess();
        }
    }
}