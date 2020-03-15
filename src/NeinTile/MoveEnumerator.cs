using System;
using System.Collections;
using System.Collections.Generic;

namespace NeinTile
{
    public struct MoveEnumerator : IEnumerator<TileMove>
    {
        private readonly TileInfo[,,] tiles;
        private readonly MoveDirection direction;

        private bool initial;
        private MoveData col;
        private MoveData row;
        private MoveData low;

        public MoveEnumerator(TileInfo[,,] tiles, MoveDirection direction)
        {
            this.tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));
            this.direction = direction;

            initial = false;

            col = new MoveData(0, tiles.GetLength(0), direction);
            row = new MoveData(1, tiles.GetLength(1), direction);
            low = new MoveData(2, tiles.GetLength(2), direction);

            Reset();
        }

        public MoveMarking Update(TileInfo source, TileInfo target)
        {
            tiles[col.Index, row.Index, low.Index] = source;
            tiles[col.ShiftIndex, row.ShiftIndex, low.ShiftIndex] = target;

            return new MoveMarking(col.ShiftMark, row.ShiftMark, low.ShiftMark);
        }

        public TileMove Current
            => new TileMove(
                tiles[col.Index, row.Index, low.Index],
                tiles[col.ShiftIndex, row.ShiftIndex, low.ShiftIndex]
            );

        object? IEnumerator.Current
            => Current;

        public bool MoveNext()
        {
            if (initial)
            {
                initial = false;
                return true;
            }

            if (col.MoveNext())
                return true;
            col.Reset();

            if (row.MoveNext())
                return true;
            row.Reset();

            if (low.MoveNext())
                return true;
            return false;
        }

        public void Reset()
        {
            initial = true;

            col.Reset();
            row.Reset();
            low.Reset();
        }

        void IDisposable.Dispose()
            => Reset();
    }
}
