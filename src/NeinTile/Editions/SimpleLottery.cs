using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class SimpleLottery : ILottery
    {
        private readonly DeterministicHeuristic heuristic;

        public SimpleLottery()
            : this(DeterministicHeuristic.Create())
        {
        }

        private SimpleLottery(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public (TileHint, Tile)? Draw(long maxValue)
        {
            var bonus = new Tile(4, 6);
            var random = heuristic.Roll();
            return random.Next(8) == 0
                ? (new TileHint(bonus), bonus)
                : ((TileHint, Tile)?)null;
        }

        public ILottery CreateNext()
            => new SimpleLottery(heuristic.Next());
    }
}
