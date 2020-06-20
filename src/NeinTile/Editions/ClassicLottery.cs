using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class ClassicLottery : ILottery
    {
        private readonly DeterministicHeuristic heuristic;

        public ClassicLottery()
            : this(DeterministicHeuristic.Create())
        {
        }

        private ClassicLottery(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public (TileHint, Tile)? Draw(long maxValue)
        {
            var random = heuristic.Roll();

            // Has chance?
            var minBonus = new Tile(6, 9);
            var maxBonusValue = maxValue / 8;

            // Is lucky?
            var nope = random.Next(21) != 0;
            if (nope || minBonus.Value > maxBonusValue)
                return null;

            // Is single?
            var pool = GetPool(minBonus, maxBonusValue);
            if (pool.Length == 1)
                return (new TileHint(pool[0]), pool[0]);

            // Is either?
            var index = random.Next(pool.Length);
            if (pool.Length == 2)
                return (new TileHint(pool[0], pool[1]), pool[index]);

            // Is random?
            var position = random.Next(
                Math.Max(1, index - pool.Length + 4),
                Math.Min(3, index + 1)
            );
            var threes = new TileHint(
                pool[index - position + 1],
                pool[index - position + 2],
                pool[index - position + 3]
            );
            return (threes, pool[index]);
        }

        private static Tile[] GetPool(Tile minBonus, long maxBonusValue)
        {
            var length = 1;
            for (var value = minBonus.Value; value < maxBonusValue; value *= 2)
            {
                length += 1;
            }
            var pool = new Tile[length];
            pool[0] = minBonus;
            for (var index = 1; index < pool.Length; index++)
            {
                var (lastValue, lastScore) = pool[index - 1];
                pool[index] = new Tile(lastValue * 2, lastScore * 3);
            }
            return pool;
        }

        public ILottery CreateNext()
            => new ClassicLottery(heuristic.Next());
    }
}
