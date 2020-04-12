using System;
using System.Runtime.InteropServices;

#pragma warning disable CA1822

namespace NeinTile.Shell
{
    public sealed class ShellView : IDisposable
    {
        private static bool DoResize => !Console.IsOutputRedirected
            && RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private readonly int bufferWidth;
        private readonly int bufferHeight;

        private readonly bool cursorVisible;

        private readonly int windowWidth;
        private readonly int windowHeight;

        public ShellView(int width, int height)
        {
            if (DoResize)
            {
                bufferWidth = Console.BufferWidth;
                bufferHeight = Console.BufferHeight;
                cursorVisible = Console.CursorVisible;
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;

                Console.SetWindowSize(1, 1);
                Console.SetBufferSize(width + 2, height + 4);
                Console.SetWindowSize(width + 2, height + 4);
                Console.CursorVisible = false;
            }

            Console.Clear();
        }

        public void Print(string content)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(content);
        }

        public ConsoleKeyInfo Next()
            => Console.ReadKey(true);

        public void Nope()
            => Console.Beep();

        public void Clear()
            => Console.Clear();

        public void Dispose()
        {
            Console.Clear();

            if (DoResize)
            {
                Console.SetWindowSize(1, 1);
                Console.SetBufferSize(bufferWidth, bufferHeight);
                Console.SetWindowSize(windowWidth, windowHeight);
                Console.CursorVisible = cursorVisible;
            }
        }
    }
}
