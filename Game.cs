using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using EasyConsole;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace task3
{
    public class Game
    {
        public string[] Moves;

        private string Key;

        public byte[] HMAC;

        private int ComputerMoveIndex;


        public Game(string[] moves)
        {
            Moves = moves;
        }

        public void MakeComputerMove()
        {
            Key = RandomNumberGenerator.GetHexString(64, false);
            ComputerMoveIndex = RandomNumberGenerator.GetInt32(0, Moves.Length);
            HMAC = new HMACSHA256(GetStringBytes(Key)).ComputeHash(GetStringBytes(Moves[ComputerMoveIndex]));
            Console.WriteLine("HMAC: " + Convert.ToHexString(HMAC));
        }

        private byte[] GetStringBytes(string message)
        {
            return Encoding.Default.GetBytes(message);
        }

        public void CreateMenu()
        {
            var menu = new EasyConsole.Menu();
            foreach (var move in Moves)
            {
                menu.Add(move, () => MakePlayerMove(Array.IndexOf(Moves, move)));
            }
            menu.Add("Help", () => ShowHelp());
            menu.Add("Exit", () => Exit());
            menu.Display();
        }
        public void ShowHelp()
        {
            var table = new Table().AddColumn(" v PC\\User >").AddColumns(Moves);
            foreach (string move in Moves)
            {
                table.AddRow(GetResultRow(Array.IndexOf(Moves,move)));
            }
            AnsiConsole.Write(table);
            CreateMenu();
        }

        private IEnumerable<IRenderable> GetResultRow(int moveIndex)
        {
            var resultRow = new List<Text>();
            resultRow.Add(new Text(Moves[moveIndex]));
            foreach (string move in Moves)
            {
                resultRow.Add(new Text(GetResult(Array.IndexOf(Moves, move), moveIndex)));
            }
            return resultRow;
        }

        public void MakePlayerMove(int playerMoveIndex)
        {
            Console.WriteLine("You move: " + Moves[playerMoveIndex] +
                              "\nComputer move: " + Moves[ComputerMoveIndex] +
                              "\nResult: " + GetResult(playerMoveIndex, ComputerMoveIndex) +
                              "\nHMAC Key: " + Key);

        }

        public string GetResult(int playerMoveIndex, int computerMoveIndex)
        {
            return playerMoveIndex == computerMoveIndex ? "Draw!" : MathMod(playerMoveIndex - computerMoveIndex, Moves.Length) <= Moves.Length / 2 ? "Win!" : "Lose!";

        }

        private static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }

        private void Exit()
        {
            return;
        }


    }
}
