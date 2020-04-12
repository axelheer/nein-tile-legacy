using System;

namespace NeinTile.Shell
{
    public static class GameLoop
    {
        public static int Run(GameState gameState)
        {
            if (gameState is null)
                throw new ArgumentNullException(nameof(gameState));

            var layerCount = gameState.Area.LayCount;
            var gamePrinter = new GamePrinter(gameState);

            using var view = new ShellView(gamePrinter.Width, gamePrinter.Height);

            var (saved, playing) = (false, true);
            var layerIndex = layerCount - 1;

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

                    case ConsoleKey.D2 when layerCount > 1:
                        layerIndex = 1;
                        break;

                    case ConsoleKey.D3 when layerCount > 2:
                        layerIndex = 2;
                        break;

                    case ConsoleKey.D4 when layerCount > 3:
                        layerIndex = 3;
                        break;

                    case ConsoleKey.D5 when layerCount > 4:
                        layerIndex = 4;
                        break;

                    case ConsoleKey.D6 when layerCount > 5:
                        layerIndex = 5;
                        break;

                    case ConsoleKey.D7 when layerCount > 6:
                        layerIndex = 6;
                        break;

                    case ConsoleKey.D8 when layerCount > 7:
                        layerIndex = 7;
                        break;

                    case ConsoleKey.D9 when layerCount > 8:
                        layerIndex = 8;
                        break;

                    case ConsoleKey.D0 when layerCount > 9:
                        layerIndex = 9;
                        break;

                    case ConsoleKey.S when next.Modifiers == ConsoleModifiers.Control:
                        using (var stream = ShellProfile.GameState.OpenWrite())
                        {
                            gameState.Save(stream);
                        }
                        saved = true;
                        break;

                    case ConsoleKey.L when next.Modifiers == ConsoleModifiers.Control && saved:
                        using (var stream = ShellProfile.GameState.OpenRead())
                        {
                            gameState = GameState.Load(stream);
                        }
                        break;

                    case ConsoleKey.R when next.Modifiers == ConsoleModifiers.Control:
                        view.Clear();
                        break;

                    case ConsoleKey.Q when next.Modifiers == ConsoleModifiers.Control:
                        playing = false;
                        break;

                    default:
                        view.Nope();
                        break;
                }
            }

            return 0;
        }
    }
}
