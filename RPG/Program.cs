using Processors;
namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            MainProcessor main = new MainProcessor();
            main.MainProcess();
        }
    }
}