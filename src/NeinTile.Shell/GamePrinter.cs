using System;
using System.Text;

namespace NeinTile.Shell
{
    public sealed class GamePrinter
    {
        public const int TileWidth = 9;
        public const int TileHeight = 3;
        public const int TileSize = TileWidth * TileHeight;

        public int ColCount { get; }
        public int RowCount { get; }

        public int Width { get; }
        public int Height { get; }

        public GamePrinter(int colCount, int rowCount)
        {
            ColCount = colCount;
            RowCount = rowCount;

            Width = ColCount * (TileWidth + 1) + 1;
            Height = RowCount * (TileHeight + 1) + 1;

            printer = new StringBuilder();
            tilesBuffer = new string[RowCount, ColCount];
        }

        private const char LeftTop = '┌';
        private const char RightTop = '┐';
        private const char LeftBottom = '└';
        private const char RightBottom = '┘';

        private const char Left = '├';
        private const char Right = '┤';
        private const char Top = '┬';
        private const char Bottom = '┴';

        private const char Crossing = '┼';
        private const char Vertical = '│';
        private const char Horizontal = '─';

        private readonly StringBuilder printer;
        private readonly string[,] tilesBuffer;

        public string Print(GameState gameState, int layerIndex)
        {
            if (gameState is null)
                throw new ArgumentNullException(nameof(gameState));

            _ = printer.Clear();

            PrepareTilesBuffer(gameState.Area, layerIndex);

            PrintHeader(gameState.Deck.Hint(), !gameState.CanMove());
            PrintSeparator(LeftTop, Top, RightTop);
            PrintTilesRow(0);
            for (var rowIndex = 1; rowIndex < RowCount; rowIndex++)
            {
                PrintSeparator(Left, Crossing, Right);
                PrintTilesRow(rowIndex);
            }
            PrintSeparator(LeftBottom, Bottom, RightBottom);
            PrintFooter(gameState.Area.TotalScore, layerIndex, gameState.Area.LayCount);

            return printer.ToString();
        }

        private void PrintTilesRow(int rowIndex)
        {
            for (var lineIndex = 0; lineIndex < TileHeight; lineIndex++)
            {
                _ = printer.Append(Vertical);
                for (var colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    _ = printer.Append(tilesBuffer[rowIndex, colIndex]
                        .Substring(lineIndex * TileWidth, TileWidth));
                    _ = printer.Append(Vertical);
                }
                _ = printer.AppendLine();
            }
        }

        private void PrintSeparator(char first, char step, char last)
        {
            _ = printer.Append(first)
                       .Append(Horizontal, TileWidth);
            for (var colIndex = 1; colIndex < ColCount; colIndex++)
            {
                _ = printer.Append(step)
                           .Append(Horizontal, TileWidth);
            }
            _ = printer.Append(last)
                       .AppendLine();
        }

        private void PrintFooter(long totalScore, int layerIndex, int layerCount)
        {
            var scoreInfo = $"Score: {totalScore:N0}";
            var layerInfo = $"Layer: {layerIndex + 1:N0} / {layerCount:N0}";
            var infoSpaces = Math.Max(1, Width - 2 - scoreInfo.Length - layerInfo.Length);
            _ = printer.Append(' ')
                       .Append(scoreInfo)
                       .Append(' ', infoSpaces)
                       .Append(layerInfo)
                       .Append(' ')
                       .AppendLine();
        }

        private void PrintHeader(TileSample hint, bool done)
        {
            var deckInfo = done ? "Done." : $"Next: {hint.First.Value:N0}";
            if (!done && !hint.IsSingle)
            {
                deckInfo += $" / {hint.Second.Value:N0}";
                if (!hint.IsEither)
                    deckInfo += $" / {hint.Third.Value:N0}";
            }
            var leftSpaces = Math.Max(1, Width - deckInfo.Length) / 2;
            var rightSpaces = Math.Max(1, Width - deckInfo.Length - leftSpaces);
            _ = printer.Append(' ', leftSpaces)
                       .Append(deckInfo)
                       .Append(' ', rightSpaces)
                       .AppendLine();
        }

        private void PrepareTilesBuffer(TilesArea tilesArea, int layerIndex)
        {
            for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                for (var colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    var tile = tilesArea[colIndex, RowCount - rowIndex - 1, layerIndex];
                    tilesBuffer[rowIndex, colIndex] = Stringify(tile);
                }
            }
        }

        private static string Stringify(TileInfo tile)
        {
            var tileInfo = tile != TileInfo.Empty ? $"{tile.Value:N0}" : "";
            return tileInfo.PadRight((TileSize - tileInfo.Length) / 2 + tileInfo.Length)
                           .PadLeft(TileSize);
        }
    }
}
