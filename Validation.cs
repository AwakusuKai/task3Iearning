using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    static class Validation
    {
        public static bool CheckArguments(string[] arguments)
        {
            return (CheckNumber(arguments) && CheckOdd(arguments) && CheckUniqueness(arguments));
        }

        private static bool CheckNumber(string[] arguments)
        {
            if (arguments.Count() < 3)
            {
                Console.WriteLine(Messages.NotEnoughParametersMessage);
                return false;
            }
            return true;
        }

        private static bool CheckOdd(string[] arguments)
        {
            if (arguments.Length % 2 == 0)
            {
                Console.WriteLine(Messages.EvenParametersNumberMessage);
                return false;
            }
            return true;
        }

        private static bool CheckUniqueness(string[] arguments)
        {
            if (arguments.Count() != arguments.Distinct().Count())
            {
                Console.WriteLine(Messages.NonUniqueParametersMessage);
                return false;
            }
            return true;
        }
    }
}
