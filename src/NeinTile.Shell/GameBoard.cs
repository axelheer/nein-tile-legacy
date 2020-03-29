using System;
using System.Globalization;
using System.Text;

namespace NeinTile.Shell
{
    public sealed class GameBoard
    {
        public const int TileWidth = 8;
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

        private const char CornerLeftTop = '┌';
        private const char CornerRightTop = '┐';
        private const char CornerLeftBottom = '└';
        private const char CornerRightBottom = '┘';

        private const char StepLeft = '├';
        private const char StepRight = '┤';
        private const char StepTop = '┬';
        private const char StepBottom = '┴';

        private const char Crossing = '┼';
        private const char Vertical = '│';
        private const char Horizontal = '─';

        public string Print()
        {
            var tiles = PrintTiles();

            var text = new StringBuilder();
            PrintDeckInfo(text);
            PrintSeparator(text, CornerLeftTop, StepTop, CornerRightTop);
            PrintRow(text, tiles, 0);
            for (var rowIndex = 1; rowIndex < RowCount; rowIndex++)
            {
                PrintSeparator(text, StepLeft, Crossing, StepRight);
                PrintRow(text, tiles, rowIndex);
            }
            PrintSeparator(text, CornerLeftBottom, StepBottom, CornerRightBottom);
            PrintTotalScoreWithLayer(text);
            return text.ToString();
        }

        private void PrintRow(StringBuilder text, string[,] tiles, int rowIndex)
        {
            for (var lineIndex = 0; lineIndex < TileHeight; lineIndex++)
            {
                _ = text.Append(Vertical);
                for (var colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    _ = text.Append(tiles[rowIndex, colIndex].Substring(lineIndex * TileWidth, TileWidth));
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

        private void PrintTotalScoreWithLayer(StringBuilder text)
        {
            var totalScore = "Total score: " + gameState.Area.TotalScore.ToString("N0", CultureInfo.CurrentCulture);
            var currentLayer = "Layer: " + (layerIndex + 1).ToString("N0", CultureInfo.CurrentCulture);

            _ = text.Append(" ")
                    .Append(totalScore)
                    .Append(' ', Width - 2 - totalScore.Length - currentLayer.Length)
                    .Append(currentLayer)
                    .Append(" ");
            _ = text.AppendLine();
        }

        private void PrintDeckInfo(StringBuilder text)
        {
            var hint = gameState.Deck.Hint();

            var deckInfo = "Possible next value(s): ";
            deckInfo += hint.First.Value.ToString("N0", CultureInfo.CurrentCulture);
            if (!hint.IsSingle)
            {
                deckInfo += ",  ";
                deckInfo += hint.Second.Value.ToString("N0", CultureInfo.CurrentCulture);
                if (!hint.IsEither)
                {
                    deckInfo += ",  ";
                    deckInfo += hint.Third.Value.ToString("N0", CultureInfo.CurrentCulture);
                }
            }
            var leftSpaces = (Width - deckInfo.Length) / 2;

            _ = text.Append(' ', leftSpaces)
                    .Append(deckInfo)
                    .Append(' ', Width - deckInfo.Length - leftSpaces);
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
            var text = tile != TileInfo.Empty ? tile.Value.ToString("N0", CultureInfo.CurrentCulture) : "";
            return text.PadLeft((TileWidth * TileHeight - text.Length) / 2 + text.Length)
                       .PadRight(TileWidth * TileHeight);
        }

        public void Move(MoveDirection direction)
            => gameState = gameState.Move(direction);

        public void Undo()
            => gameState = gameState.Previous ?? gameState;

        public void Switch()
            => layerIndex = (layerIndex + 1) % gameState.Area.LayCount;
    }
}
