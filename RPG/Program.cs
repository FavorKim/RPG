using Processors;
using RPG.Select.Selectors;

namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            IntroSelP selP = new IntroSelP();
            selP.selP.SelectVoid();
        }
    }
}