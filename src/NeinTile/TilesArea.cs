using System;
using System.Collections.Generic;

namespace NeinTile
{
    public class TilesArea
    {
        private static readonly TileInfo[,,] emptyTiles = new TileInfo[0, 0, 0];

        private readonly ITilesAreaMixer mixer;
        private readonly ITilesAreaMerger merger;
        private readonly ITilesAreaLottery lottery;

        private readonly TileInfo[,,] tiles;

        public TilesArea(ITilesAreaMixer mixer, ITilesAreaMerger merger, ITilesAreaLottery lottery)
            : this(mixer, merger, lottery, emptyTiles)
        {
        }

        private TilesArea(ITilesAreaMixer mixer, ITilesAreaMerger merger, ITilesAreaLottery lottery, TileInfo[,,] tiles)
        {
            this.mixer = mixer ?? throw new ArgumentNullException(nameof(mixer));
            this.merger = merger ?? throw new ArgumentNullException(nameof(merger));
            this.lottery = lottery ?? throw new ArgumentNullException(nameof(lottery));

            this.tiles = tiles.Length == 0 ? mixer.Tiles : tiles;
        }

        public int ColCount
            => tiles.GetLength(0);

        public int RowCount
            => tiles.GetLength(1);

        public int LowCount
            => tiles.GetLength(2);

        public TileInfo this[int colIndex, int rowIndex, int lowIndex]
            => tiles[colIndex, rowIndex, lowIndex];

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
            var nextTiles = new TileInfo[ColCount, RowCount, LowCount];

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

            var (colIndex, rowIndex, lowIndex) = lottery.Pick(markings.ToArray());
            nextTiles[colIndex, rowIndex, lowIndex] = nextTile;

            return new TilesArea(mixer.Shuffle(), merger, lottery.Draw(), nextTiles);
        }
    }
}
