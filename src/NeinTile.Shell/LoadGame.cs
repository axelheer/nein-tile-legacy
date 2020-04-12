using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

#pragma warning disable CA1822 // Must not be static

namespace NeinTile.Shell
{
    [Command("load", Description = "Load an existing game")]
    public class LoadGame
    {
        public async Task<int> OnExecuteAsync(IConsole console)
        {
            if (console is null)
                throw new ArgumentNullException(nameof(console));

            if (!ShellProfile.GameState.Exists)
            {
                _ = console.WriteLine("You must save a game first.");
                return 1;
            }

            using var stream = ShellProfile.GameState.OpenRead();
            var gameState = await GameState.LoadAsync(stream);

            return await GameLoop.Run(gameState);
        }
    }
}
