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

        [AllowedValues("s", "simple", "c", "classic", "d", "duality", "i", "insanity")]
        [Option(Description = "Edition to play (defaults to 'classic')\nAllowed values are: s[imple], c[lassic], d[uality], i[nsanity]")]
        public string Edition { get; set; } = "classic";

        [Range(1, 12)]
        [Option(Description = "Number of columns (defaults to 4)")]
        public int Columns { get; set; } = 4;

        [Range(1, 12)]
        [Option(Description = "Number of rows (defaults to 4)")]
        public int Rows { get; set; } = 4;

        [Range(1, 12)]
        [Option(Description = "Number of layers (defaults to 1)")]
        public int Layers { get; set; } = 1;

        private void OnExecute()
        {
            var playing = true;
            var layerIndex = Layers - 1;

            var gamePrinter = new GamePrinter(Columns, Rows);
            var gameOptions = new GameOptions(Columns, Rows, Layers);
            var gameState = GameFactory.CreateNew(Edition, gameOptions);

            using var view = new GameView(gamePrinter.Width, gamePrinter.Height);

            while (playing)
            {
                var content = gamePrinter.Print(gameState, layerIndex);
                view.Print(content);

                var next = view.Next();
                switch (next.Key)
                {
                    case ConsoleKey.RightArrow when gameState.CanMove(MoveDirection.Right):
                        gameState = gameState.Move(MoveDirection.Right);
                        break;

                    case ConsoleKey.LeftArrow when gameState.CanMove(MoveDirection.Left):
                        gameState = gameState.Move(MoveDirection.Left);
                        break;

                    case ConsoleKey.UpArrow when gameState.CanMove(MoveDirection.Up):
                        gameState = gameState.Move(MoveDirection.Up);
                        break;

                    case ConsoleKey.DownArrow when gameState.CanMove(MoveDirection.Down):
                        gameState = gameState.Move(MoveDirection.Down);
                        break;

                    case ConsoleKey.F when gameState.CanMove(MoveDirection.Forward):
                        gameState = gameState.Move(MoveDirection.Forward);
                        break;

                    case ConsoleKey.B when gameState.CanMove(MoveDirection.Backward):
                        gameState = gameState.Move(MoveDirection.Backward);
                        break;

                    case ConsoleKey.U when gameState.Previous is { }:
                        gameState = gameState.Previous;
                        break;

                    case ConsoleKey.D1:
                        layerIndex = 0;
                        break;

                    case ConsoleKey.D2 when Layers > 1:
                        layerIndex = 1;
                        break;

                    case ConsoleKey.D3 when Layers > 2:
                        layerIndex = 2;
                        break;

                    case ConsoleKey.D4 when Layers > 3:
                        layerIndex = 3;
                        break;

                    case ConsoleKey.D5 when Layers > 4:
                        layerIndex = 4;
                        break;

                    case ConsoleKey.D6 when Layers > 5:
                        layerIndex = 5;
                        break;

                    case ConsoleKey.D7 when Layers > 6:
                        layerIndex = 6;
                        break;

                    case ConsoleKey.D8 when Layers > 7:
                        layerIndex = 7;
                        break;

                    case ConsoleKey.D9 when Layers > 8:
                        layerIndex = 8;
                        break;

                    case ConsoleKey.D0 when Layers > 9:
                        layerIndex = 9;
                        break;

                    case ConsoleKey.R:
                        view.Clear();
                        break;

                    case ConsoleKey.Q when next.Modifiers != default:
                        playing = false;
                        break;

                    default:
                        view.Nope();
                        break;
                }
            }
        }
    }
}
