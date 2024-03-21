using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    class InputProcessor
    {
        public static int Input(int length)
        {
            int input = length + 1;
            while (input < 0 || input > length)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Wrong Input. TryAgain");
                }
            }

            return input;
        }
    }
}
