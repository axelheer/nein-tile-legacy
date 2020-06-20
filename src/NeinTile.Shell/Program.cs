using System;
using McMaster.Extensions.CommandLineUtils;

#pragma warning disable CA1822 // Must not be static

namespace NeinTile.Shell
{
    [Command("neintile", Description = "*NEW* Download the App: https://apps.apple.com/app/apple-store/id1518189085")]
    [Subcommand(typeof(LoadGame), typeof(PlayGame))]
    public sealed class Program
    {
        public static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));
            if (console is null)
                throw new ArgumentNullException(nameof(console));

            _ = console.WriteLine("You must specify at a subcommand.");
            app.ShowHelp();
            return 1;
        }
    }
}
