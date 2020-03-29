using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace NeinTile.Shell
{
    [Command("neintile", Description = "Just playing around...")]
    public sealed class Program
    {
        public static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [AllowedValues("s", "simple", "c", "classic", "i", "insanity")]
        [Option(Description = "Edition to play (defaults to 'classic')\nAllowed values are: s[imple], c[lassic], i[nsanity]")]
        public string Edition { get; set; } = "classic";

        [Range(1, 16)]
        [Option(Description = "Number of columns (defaults to 4)")]
        public int Columns { get; set; } = 4;

        [Range(1, 16)]
        [Option(Description = "Number of rows (defaults to 4)")]
        public int Rows { get; set; } = 4;

        [Range(1, 16)]
        [Option(Description = "Number of layers (defaults to 1)")]
        public int Layers { get; set; } = 1;

        private void OnExecute()
        {
            var gameOptions = new GameOptions(Columns, Rows, Layers);
            var gameState = GameFactory.CreateNew(Edition, gameOptions);
            var gameBoard = new GameBoard(gameState);

            Console.Clear();

            do
            {
                var text = gameBoard.Print();

                Console.SetCursorPosition(0, 0);
                Console.Write(text);
            }
            while (OnKeyDown(gameBoard, Console.ReadKey(true)));
        }

        private static bool OnKeyDown(GameBoard gameBoard, ConsoleKeyInfo next)
        {
            if (next.Modifiers != default && next.Key == ConsoleKey.Q)
                return false;

            switch (next.Key)
            {
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    gameBoard.Move(MoveDirection.Right);
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    gameBoard.Move(MoveDirection.Left);
                    break;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    gameBoard.Move(MoveDirection.Up);
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    gameBoard.Move(MoveDirection.Down);
                    break;

                case ConsoleKey.R:
                    gameBoard.Move(MoveDirection.Forward);
                    break;

                case ConsoleKey.F:
                    gameBoard.Move(MoveDirection.Backward);
                    break;

                case ConsoleKey.X:
                    gameBoard.Scroll();
                    break;

                case ConsoleKey.Backspace:
                    gameBoard.Undo();
                    break;

                default:
                    Console.Beep();
                    break;
            }

            return true;
        }
    }
}
