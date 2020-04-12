using System;
using System.IO;

namespace NeinTile.Shell
{
    public static class ShellProfile
    {
        public static DirectoryInfo Profile { get; }
            = Directory.CreateDirectory(Path.Combine(
                Environment.GetEnvironmentVariable("HOME") ?? ".",
                ".neintile")
            );

        public static FileInfo GameState { get; }
            = new FileInfo(Path.Combine(Profile.FullName, $"saved.gs1"));
    }
}
