using System;
using System.Collections.Generic;

namespace NeinTile
{
    public class TilesArea
    {
        private readonly ITilesAreaMerger merger;
        private readonly ITilesAreaLottery lottery;

        private readonly TileInfo[,,] tiles;

        public TilesArea(ITilesAreaMixer mixer, ITilesAreaMerger merger, ITilesAreaLottery lottery)
            : this(merger, lottery, (mixer ?? throw new ArgumentNullException(nameof(mixer))).Shuffle())
        {
        }

        private TilesArea(ITilesAreaMerger merger, ITilesAreaLottery lottery, TileInfo[,,] tiles)
        {
            this.merger = merger ?? throw new ArgumentNullException(nameof(merger));
            this.lottery = lottery ?? throw new ArgumentNullException(nameof(lottery));

            ColCount = tiles.GetLength(0);
            RowCount = tiles.GetLength(1);
            LowCount = tiles.GetLength(2);

            this.tiles = tiles;
        }

        public int ColCount { get; }
        public int RowCount { get; }
        public int LowCount { get; }

        public TileInfo this[int colIndex, int rowIndex, int lowIndex]
            => tiles[colIndex, rowIndex, lowIndex];

        public int MaxValue
            => Calculate<int>((value, i) => Math.Max(value, i.Value));

        public long TotalScore
            => Calculate<long>((score, i) => score + i.Score);

        private T Calculate<T>(Func<T, TileInfo, T> calculator)
            where T : struct
        {
            T value = default;
            for (var colIndex = 0; colIndex < ColCount; colIndex++)
                for (var rowIndex = 0; rowIndex < RowCount; rowIndex++)
                    for (var lowIndex = 0; lowIndex < LowCount; lowIndex++)
                        value = calculator(value, this[colIndex, rowIndex, lowIndex]);
            return value;
        }

        public virtual bool CanMove(MoveDirection direction)
        {
            var enumerator = new MoveEnumerator(tiles, direction);
            while (enumerator.MoveNext())
            {
                var (source, target) = enumerator.Current;
                if (merger.CanMerge(source, target))
                    return true;
            }
            return false;
        }

        public virtual TilesArea Move(MoveDirection direction, TileInfo nextTile)
        {
            var markings = new List<MoveMarking>();
            var nextTiles = (TileInfo[,,])tiles.Clone();

            var enumerator = new MoveEnumerator(nextTiles, direction);
            while (enumerator.MoveNext())
            {
                var (source, target) = enumerator.Current;
                if (merger.CanMerge(source, target))
                {
                    target = merger.Merge(source, target, out source);
                    markings.Add(enumerator.Update(source, target));
                }
            }

            var (colIndex, rowIndex, lowIndex) = lottery.Draw(markings.ToArray());
            nextTiles[colIndex, rowIndex, lowIndex] = nextTile;

            return new TilesArea(merger, lottery.CreateNext(), nextTiles);
        }
    }
}
