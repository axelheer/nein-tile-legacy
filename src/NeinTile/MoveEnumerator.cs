using System;
using System.Collections;
using System.Collections.Generic;

namespace NeinTile
{
    public struct MoveEnumerator : IEnumerator<Move>
    {
        private bool initial;

        private MoveData col;
        private MoveData row;
        private MoveData lay;

        public MoveEnumerator(Tiles tiles, MoveDirection direction)
        {
            if (tiles is null)
                throw new ArgumentNullException(nameof(tiles));

            col = new MoveData(0, tiles.ColCount, direction);
            row = new MoveData(1, tiles.RowCount, direction);
            lay = new MoveData(2, tiles.LayCount, direction);

            initial = true;
        }

        public Move Current
            => new Move(
                new TileIndex(col.Source, row.Source, lay.Source),
                new TileIndex(col.Target, row.Target, lay.Target),
                new TileIndex(col.Marker, row.Marker, lay.Marker)
            );

        object? IEnumerator.Current
            => Current;

        public bool MoveNext()
        {
            if (col.Blocked || row.Blocked || lay.Blocked)
                return false;
            if (initial)
                return Start();
            if (col.Next())
                return true;
            col.Reset();
            if (row.Next())
                return true;
            row.Reset();
            if (lay.Next())
                return true;
            lay.Reset();
            return false;
        }

        public void Reset()
            => initial = true;

        private bool Start()
        {
            initial = false;
            col.Reset();
            row.Reset();
            lay.Reset();
            return true;
        }

        void IDisposable.Dispose()
        {
            // nothing to do here
        }

        private struct MoveData
        {
            private readonly int step;

            private readonly int start;
            private readonly int shift;
            private readonly int count;

            public readonly bool Blocked { get; }

            public MoveData(int dim, int dimCount, MoveDirection direction)
            {
                step = (int)direction % 2 == 1 ? 1 : -1;

                var positive = dim + dim == (int)direction;
                var negative = dim + dim + 1 == (int)direction;

                start = negative ? 1 : 0;
                shift = positive ? 1 : negative ? -1 : 0;
                count = positive ? dimCount - 1 : dimCount;

                Blocked = start + shift == count + shift;

                index = 0;
            }

            private int index;

            public int Source
                => index;

            public int Target
                => index + shift;

            public int Marker
                => shift == 0 ? index : step < 0 ? 0 : count - 1;

            public bool Next()
            {
                index += step;
                return start <= index && index < count;
            }

            public void Reset()
                => index = step > 0 ? start : count - 1;
        }
    }
}
