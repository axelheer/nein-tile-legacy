using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class SimpleTilesDeckLottery : ITilesDeckLottery
    {
        private readonly DeterministicHeuristic heuristic;

        public SimpleTilesDeckLottery()
            : this(DeterministicHeuristic.CreateNew())
        {
        }

        private SimpleTilesDeckLottery(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public TileSample Draw(out TileInfo bonus)
        {
            var random = heuristic.Next();

            if (random.Next(8) != 0)
            {
                bonus = TileInfo.Empty;
                return TileSample.Empty;
            }

            bonus = new TileInfo(4, 6);
            return new TileSample(bonus);
        }

        public ITilesDeckLottery CreateNext(TilesArea? area)
            => new SimpleTilesDeckLottery(heuristic.CreateNext());
    }
}
