using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace NeinTile.Shell
{
    [Command("play", Description = "Play a new game")]
    public class PlayGame
    {
        [Option(Description = "Edition to play (defaults to 'Classic')")]
        public GameEdition Edition { get; set; } = GameEdition.Classic;

        [Range(1, 12)]
        [Option(Description = "Number of columns (defaults to 4)")]
        public int Columns { get; set; } = 4;

        [Range(1, 12)]
        [Option(Description = "Number of rows (defaults to 4)")]
        public int Rows { get; set; } = 4;

        [Range(1, 12)]
        [Option(Description = "Number of layers (defaults to 1)")]
        public int Layers { get; set; } = 1;

        public int OnExecute()
        {
            var gameOptions = new GameOptions(Columns, Rows, Layers);
            var gameState = GameFactory.CreateNew(Edition, gameOptions);

            return GameLoop.Run(gameState);
        }
    }
}
