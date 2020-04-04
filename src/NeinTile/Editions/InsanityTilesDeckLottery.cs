using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class InsanityTilesDeckLottery : ITilesDeckLottery
    {
        private readonly TilesArea? area;
        private readonly DeterministicHeuristic heuristic;

        public InsanityTilesDeckLottery()
            : this(null, DeterministicHeuristic.CreateNew())
        {
        }

        private InsanityTilesDeckLottery(TilesArea? area, DeterministicHeuristic heuristic)
        {
            this.area = area;
            this.heuristic = heuristic;
        }

        public TileSample Draw(out TileInfo bonus)
        {
            var random = heuristic.Next();

            // Has chance?
            var totalScore = area?.TotalScore ?? 0;

            // Is unlucky or poor anyway?
            var nope = random.Next(13) != 0;
            if (nope || totalScore < 1_000)
            {
                bonus = TileInfo.Empty;
                return TileSample.Empty;
            }

            // Is "lucky"?
            var pool = GetPool(1_000, totalScore);
            var index = random.Next(pool.Length);
            var bonusValue = pool[index];

            // Is rich?
            if (totalScore > 1_000_000)
            {
                bonus = new TileInfo(0, bonusValue);
                return new TileSample(bonus);
            }

            // Life is life...
            bonus = new TileInfo(0, -bonusValue);
            return new TileSample(bonus);
        }

        private static long[] GetPool(long minBonus, long maxBonus)
        {
            var length = 1;
            for (var value = minBonus; value < maxBonus; value *=2)
            {
                length += 1;
            }
            var pool = new long[length];
            pool[0] = minBonus;
            for (var index = 1; index < pool.Length; index++)
            {
                pool[index] = pool[index - 1] * 2;
            }
            return pool;
        }

        public ITilesDeckLottery CreateNext(TilesArea? area)
            => new InsanityTilesDeckLottery(area, heuristic.CreateNext());
    }
}
