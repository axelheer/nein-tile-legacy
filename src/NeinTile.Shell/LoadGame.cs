using System;
using McMaster.Extensions.CommandLineUtils;

#pragma warning disable CA1822 // Must not be static

namespace NeinTile.Shell
{
    [Command("load", Description = "Load an existing game")]
    public class LoadGame
    {
        public int OnExecute(IConsole console)
        {
            if (console is null)
                throw new ArgumentNullException(nameof(console));

            if (!ShellProfile.Game.Exists)
            {
                _ = console.WriteLine("You must save a game first.");
                return 1;
            }

            using var stream = ShellProfile.Game.OpenRead();
            var gameState = Game.Load(stream);
            stream.Close();

            return GameLoop.Run(gameState);
        }
    }
}
