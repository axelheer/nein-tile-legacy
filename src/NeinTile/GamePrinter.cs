using System;
using System.Text;

namespace NeinTile
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

            printer = new StringBuilder((Width + 2) * (Height + 2));
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
            var left = $" Score: {totalScore:N0} ";
            var right = $" Layer: {layerIndex + 1:N0} / {layerCount:N0} ";
            var footer = left.Length + right.Length <= Width
                ? left.PadRight(Width - right.Length) + right
                : left.PadRight(Width);
            _ = printer.AppendLine(footer);
        }

        private void PrintHeader(TileSample hint, bool done)
        {
            var header = done
                ? " Done. "
                : hint.IsSingle
                ? $" Next: {hint.First.Value:N0} "
                : hint.IsEither
                ? $" Next: {hint.First.Value:N0} / {hint.Second.Value:N0} "
                : $" Next: {hint.First.Value:N0} / {hint.Second.Value:N0} / {hint.Third.Value:N0} ";
            _ = printer.AppendLine(header.PadRight((Width - header.Length) / 2 + header.Length)
                                         .PadLeft(Width));
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
            var info = tile != TileInfo.Empty ? $"{tile.Value:N0}" : "";
            return info.PadRight((TileSize - info.Length) / 2 + info.Length)
                       .PadLeft(TileSize);
        }
    }
}
