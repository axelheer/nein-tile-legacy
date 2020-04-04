using System;
using System.Collections;
using System.Collections.Generic;

namespace NeinTile
{
    public struct MoveEnumerator : IEnumerator<TileMove>
    {
        private readonly TileInfo[,,] tiles;

        private bool initial;
        private MoveData col;
        private MoveData row;
        private MoveData lay;
        private bool blocked;

        public MoveEnumerator(TileInfo[,,] tiles, MoveDirection direction)
        {
            this.tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));

            initial = false;

            col = new MoveData(0, tiles.GetLength(0), direction);
            row = new MoveData(1, tiles.GetLength(1), direction);
            lay = new MoveData(2, tiles.GetLength(2), direction);

            blocked = false;

            Reset();
        }

        public MoveMarking Update(TileInfo source, TileInfo target)
        {
            tiles[col.Index, row.Index, lay.Index] = source;
            tiles[col.ShiftIndex, row.ShiftIndex, lay.ShiftIndex] = target;

            return new MoveMarking(col.ShiftMark, row.ShiftMark, lay.ShiftMark);
        }

        public TileMove Current
            => new TileMove(
                tiles[col.Index, row.Index, lay.Index],
                tiles[col.ShiftIndex, row.ShiftIndex, lay.ShiftIndex]
            );

        object? IEnumerator.Current
            => Current;

        public bool MoveNext()
        {
            if (blocked)
                return false;

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

            return lay.MoveNext();
        }

        public void Reset()
        {
            initial = true;

            col.Reset();
            row.Reset();
            lay.Reset();

            blocked = col.Blocked
                || row.Blocked
                || lay.Blocked;
        }

        void IDisposable.Dispose()
            => Reset();
    }
}
