using System;
using System.Collections;
using System.Collections.Generic;

namespace NeinTile
{
    public sealed class MoveEnumerator : IEnumerator<(TileInfo tile, TileInfo other)>
    {
        private readonly TileInfo[,,] tiles;
        private readonly MoveDirection direction;

        private readonly int colStart;
        private readonly int colShift;
        private readonly int colCount;
        private int colIndex;

        private readonly int rowStart;
        private readonly int rowShift;
        private readonly int rowCount;
        private int rowIndex;

        private readonly int lowStart;
        private readonly int lowShift;
        private readonly int lowCount;
        private int lowIndex;

        public MoveEnumerator(TileInfo[,,] tiles, MoveDirection direction)
        {
            this.tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));
            this.direction = direction;

            colCount = tiles.GetLength(0);
            rowCount = tiles.GetLength(1);
            lowCount = tiles.GetLength(2);

            switch (direction)
            {
                case MoveDirection.Right:
                    colCount -= 1;
                    colShift = +1;
                    break;

                case MoveDirection.Left:
                    colStart += 1;
                    colShift = -1;
                    break;

                case MoveDirection.Up:
                    rowCount -= 1;
                    rowShift = +1;
                    break;

                case MoveDirection.Down:
                    rowStart += 1;
                    rowShift = -1;
                    break;

                case MoveDirection.Forward:
                    lowCount -= 1;
                    lowShift = +1;
                    break;

                case MoveDirection.Backward:
                    lowStart += 1;
                    lowShift = -1;
                    break;

                default:
                    throw new NotSupportedException($"Direction '{direction}' not supported.");
            }

            Reset();
        }

        public (TileInfo tile, TileInfo other) Current
            => (tiles[colIndex, rowCount, lowIndex], tiles[colIndex + colShift, rowIndex + rowShift, lowIndex + lowShift]);

        object? IEnumerator.Current
            => Current;

        public bool MoveNext()
        {
            if (colIndex < colCount)
            {
                colIndex += 1;
                return true;
            }

            colIndex = colStart;

            if (rowIndex < rowCount)
            {
                rowIndex += 1;
                return true;
            }

            rowIndex = rowCount;

            if (lowIndex < lowCount)
            {
                lowIndex += 1;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            colIndex = colStart - 1;
            rowIndex = rowStart;
            lowIndex = lowStart;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
