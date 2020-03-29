using System;
using System.Text;

namespace NeinTile.Shell
{
    public sealed class GameBoard
    {
        public const int TileWidth = 9;
        public const int TileHeight = 5;

        private GameState gameState;
        private int layerIndex;

        public int ColCount => gameState.Area.ColCount;
        public int RowCount => gameState.Area.RowCount;

        public int Width { get; }
        public int Height { get; }

        public GameBoard(GameState gameState)
        {
            this.gameState = gameState ?? throw new ArgumentNullException(nameof(gameState));

            Width = ColCount * (TileWidth + 1) + 1;
            Height = RowCount * (TileHeight + 1) + 1;
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

        public string Print()
        {
            var tiles = PrintTiles();

            var text = new StringBuilder();
            PrintHeader(text);
            PrintSeparator(text, LeftTop, Top, RightTop);
            PrintTileRow(text, tiles, 0);
            for (var rowIndex = 1; rowIndex < RowCount; rowIndex++)
            {
                PrintSeparator(text, Left, Crossing, Right);
                PrintTileRow(text, tiles, rowIndex);
            }
            PrintSeparator(text, LeftBottom, Bottom, RightBottom);
            PrintFooter(text);
            return text.ToString();
        }

        private void PrintTileRow(StringBuilder text, string[,] tiles, int rowIndex)
        {
            for (var lineIndex = 0; lineIndex < TileHeight; lineIndex++)
            {
                _ = text.Append(Vertical);
                for (var colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    _ = text.Append(tiles[rowIndex, colIndex]
                        .Substring(lineIndex * TileWidth, TileWidth));
                    _ = text.Append(Vertical);
                }
                _ = text.AppendLine();
            }
        }

        private void PrintSeparator(StringBuilder text, char first, char step, char last)
        {
            for (var colIndex = 0; colIndex < ColCount; colIndex++)
            {
                _ = colIndex == 0
                    ? text.Append(first)
                    : text;
                _ = text.Append(Horizontal, TileWidth);
                _ = colIndex == ColCount - 1
                    ? text.Append(last)
                    : text.Append(step);
            }
            _ = text.AppendLine();
        }

        private string[,] PrintTiles()
        {
            var result = new string[RowCount, ColCount];
            for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                for (var colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    var tile = gameState.Area[colIndex, RowCount - rowIndex - 1, layerIndex];
                    result[rowIndex, colIndex] = PrintTile(tile);
                }
            }
            return result;
        }

        private static string PrintTile(TileInfo tile)
        {
            var tileInfo = tile != TileInfo.Empty ? $"{tile.Value:N0}" : "";

            return tileInfo.PadRight((TileWidth * TileHeight - tileInfo.Length) / 2 + tileInfo.Length)
                           .PadLeft(TileWidth * TileHeight);
        }

        private void PrintFooter(StringBuilder text)
        {
            var scoreInfo = $"Score: {gameState.Area.TotalScore:N0}";
            var layerInfo = $"Layer: {layerIndex + 1:N0}";

            _ = text.Append(' ')
                    .Append(scoreInfo)
                    .Append(' ', Width - 2 - scoreInfo.Length - layerInfo.Length)
                    .Append(layerInfo)
                    .Append(' ');
            _ = text.AppendLine();
        }

        private void PrintHeader(StringBuilder text)
        {
            var hint = gameState.Deck.Hint();

            var deckInfo = $"Preview: {hint.First.Value:N0}";
            if (!hint.IsSingle)
            {
                deckInfo += $", {hint.Second.Value:N0}";
                if (!hint.IsEither)
                {
                    deckInfo += $", {hint.Third.Value:N0}";
                }
            }
            var leftSpaces = (Width - deckInfo.Length) / 2;

            _ = text.Append(' ', leftSpaces)
                    .Append(deckInfo)
                    .Append(' ', Width - deckInfo.Length - leftSpaces);
            _ = text.AppendLine();
        }

        public void Move(MoveDirection direction)
            => gameState = gameState.Move(direction);

        public void Undo()
            => gameState = gameState.Previous ?? gameState;

        public void Scroll()
            => layerIndex = (layerIndex + 1) % gameState.Area.LayCount;
    }
}
