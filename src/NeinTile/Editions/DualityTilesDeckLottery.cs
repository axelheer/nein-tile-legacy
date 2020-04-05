using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DualityTilesDeckLottery : ITilesDeckLottery
    {
        private readonly DeterministicHeuristic heuristic;

        public DualityTilesDeckLottery()
            : this(DeterministicHeuristic.CreateNew())
        {
        }

        private DualityTilesDeckLottery(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public TileSample Draw(out TileInfo bonus)
        {
            var random = heuristic.Next();

            if (random.Next(4) != 0)
            {
                bonus = TileInfo.Empty;
                return TileSample.Empty;
            }

            bonus = new TileInfo(0, 100);
            return new TileSample(bonus);
        }

        public ITilesDeckLottery CreateNext(TilesArea? area)
            => new DualityTilesDeckLottery(heuristic.CreateNext());
    }
}
