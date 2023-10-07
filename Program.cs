using System;
using System.Linq;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Validation.CheckArguments(args))
            {
                Game game = new Game(args);
                game.MakeComputerMove();
                game.CreateMenu();
            }
            return;
            
        }

        
    }
}
