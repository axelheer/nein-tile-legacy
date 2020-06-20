using System;

namespace NeinTile.Shell
{
    public static class GameLoop
    {
        public static int Run(Game game)
        {
            if (game is null)
                throw new ArgumentNullException(nameof(game));

            var layerCount = game.Area.Tiles.LayCount;
            var printer = new Printer(game);

            using var view = new ShellView(printer.Width, printer.Height);

            var (saved, playing) = (false, true);
            var layerIndex = layerCount - 1;

            while (playing)
            {
                if (game is null)
                    throw new ArgumentNullException(nameof(game));

                var content = printer.Print(game, layerIndex);
                view.Print(content);

                var next = view.Next();
                switch (next.Key)
                {
                    case ConsoleKey.RightArrow when game.CanMove(MoveDirection.Right):
                        game = game.Move(MoveDirection.Right);
                        break;

                    case ConsoleKey.LeftArrow when game.CanMove(MoveDirection.Left):
                        game = game.Move(MoveDirection.Left);
                        break;

                    case ConsoleKey.UpArrow when game.CanMove(MoveDirection.Up):
                        game = game.Move(MoveDirection.Up);
                        break;

                    case ConsoleKey.DownArrow when game.CanMove(MoveDirection.Down):
                        game = game.Move(MoveDirection.Down);
                        break;

                    case ConsoleKey.F when game.CanMove(MoveDirection.Front):
                        game = game.Move(MoveDirection.Back);
                        break;

                    case ConsoleKey.B when game.CanMove(MoveDirection.Back):
                        game = game.Move(MoveDirection.Back);
                        break;

                    case ConsoleKey.U when game.Previous is { }:
                        game = game.Previous;
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
                        using (var stream = ShellProfile.Game.OpenWrite())
                        {
                            game.Save(stream);
                        }
                        saved = true;
                        break;

                    case ConsoleKey.L when next.Modifiers == ConsoleModifiers.Control && saved:
                        using (var stream = ShellProfile.Game.OpenRead())
                        {
                            game = Game.Load(stream);
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
