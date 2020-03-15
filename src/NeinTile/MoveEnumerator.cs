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

        private struct MoveData
        {
            private readonly int direction;

            public readonly int Start { get; }
            public readonly int Shift { get; }
            public readonly int Count { get; }

            public int Index { get; set; }

            public MoveData(int dimension, int count, MoveDirection direction)
            {
                this.direction = (int)direction % 2 == 1 ? 1 : -1;

                var positive = dimension + dimension == (int)direction;
                var negative = dimension + dimension + 1 == (int)direction;

                Start = negative ? 1 : 0;
                Shift = positive ? 1 : negative ? -1 : 0;
                Count = positive ? count - 1 : count;

                Index = -1;
            }

            public int ShiftIndex
                => Index + Shift;

            public int ShiftMark
                => Shift == 0 ? Index : direction < 0 ? 0 : Count - 1;

            public void Reset()
                => Index = direction > 0 ? Start : Count - 1;

            public bool MoveNext()
            {
                Index += direction;

                return Start <= Index && Index < Count;
            }
        }
    }
}
