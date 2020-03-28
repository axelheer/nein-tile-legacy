using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace NeinTile.Shell
{
    [Command("neintile", Description = "Just playing around...")]
    public class Program
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
            => GameFactory.CreateNew(Edition, new GameOptions(Columns, Rows, Layers));
    }
}
