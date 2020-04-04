using System;
using System.Runtime.InteropServices;

#pragma warning disable CA1822

namespace NeinTile.Shell
{
    public sealed class GameView : IDisposable
    {
        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private readonly int bufferWidth;
        private readonly int bufferHeight;

        private readonly bool cursorVisible;

        private readonly int windowWidth;
        private readonly int windowHeight;

        public GameView(int width, int height)
        {
            if (IsWindows)
            {
                bufferWidth = Console.BufferWidth;
                bufferHeight = Console.BufferHeight;
                cursorVisible = Console.CursorVisible;
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;

                Console.SetWindowSize(1, 1);
                Console.SetBufferSize(width, height + 4);
                Console.SetWindowSize(width, height + 4);
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

            if (IsWindows)
            {
                Console.SetWindowSize(1, 1);
                Console.SetBufferSize(bufferWidth, bufferHeight);
                Console.SetWindowSize(windowWidth, windowHeight);
                Console.CursorVisible = cursorVisible;
            }
        }
    }
}
