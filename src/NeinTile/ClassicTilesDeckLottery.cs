using System;

namespace NeinTile
{
    public sealed class ClassicTilesDeckLottery : ITilesDeckLottery
    {
        private readonly TilesArea? area;
        private readonly DeterministicHeuristic heuristic;

        public ClassicTilesDeckLottery()
            : this(null, DeterministicHeuristic.CreateNew())
        {
        }

        private ClassicTilesDeckLottery(TilesArea? area, DeterministicHeuristic heuristic)
        {
            this.area = area;
            this.heuristic = heuristic;
        }

        public TileSample Draw(out TileInfo bonus)
        {
            var random = heuristic.Next();

            // Has chance?
            var maxValue = area != null ? FindMax(area) : 0;
            var minBonus = new TileInfo(6, 9);
            var maxBonusValue = maxValue / 8;

            // Is lucky?
            var nope = random.Next(21) != 0;
            if (nope || minBonus.Value > maxBonusValue)
            {
                bonus = TileInfo.Empty;
                return TileSample.Empty;
            }

            // Is single?
            var pool = GetPool(minBonus, maxBonusValue);
            if (pool.Length == 1)
            {
                bonus = pool[0];
                return new TileSample(pool[0]);
            }

            // Is either?
            var index = random.Next(pool.Length);
            if (pool.Length == 2)
            {
                bonus = pool[index];
                return new TileSample(pool[0], pool[1]);
            }

            // Is random?
            var position = random.Next(
                Math.Max(1, index - pool.Length + 4),
                Math.Min(3, index + 1)
            );
            bonus = pool[index];
            return position switch
            {
                1 => new TileSample(pool[index], pool[index + 1], pool[index + 2]),
                2 => new TileSample(pool[index - 1], pool[index], pool[index + 1]),
                _ => new TileSample(pool[index - 2], pool[index - 1], pool[index])
            };
        }

        private static TileInfo[] GetPool(TileInfo minBonus, int maxBonusValue)
        {
            var length = 1;
            for (var value = minBonus.Value; value < maxBonusValue; value = value + value)
            {
                length = length + 1;
            }
            var pool = new TileInfo[length];
            pool[0] = minBonus;
            for (var index = 1; index < pool.Length; index++)
            {
                var (lastValue, lastScore) = pool[index - 1];
                pool[index] = new TileInfo(
                    lastValue + lastValue,
                    lastScore + lastScore + lastScore
                );
            }
            return pool;
        }

        private static int FindMax(TilesArea area)
        {
            var maxValue = 1;
            for (var colIndex = 0; colIndex < area.ColCount; colIndex++)
                for (var rowIndex = 0; rowIndex < area.RowCount; rowIndex++)
                    for (var lowIndex = 0; lowIndex < area.LowCount; lowIndex++)
                        maxValue = Math.Max(maxValue, area[colIndex, rowIndex, lowIndex].Value);
            return maxValue;
        }

        public ITilesDeckLottery CreateNext(TilesArea? area)
            => new ClassicTilesDeckLottery(area, heuristic.CreateNext());
    }
}
