using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class InsanityLottery : ILottery
    {
        private readonly DeterministicHeuristic heuristic;

        public InsanityLottery()
            : this(DeterministicHeuristic.Create())
        {
        }

        private InsanityLottery(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public (TileHint, Tile)? Draw(long maxValue)
        {
            var bonus = new Tile(0, -maxValue);
            var random = heuristic.Roll();
            return random.Next(16) == 0
                ? (new TileHint(bonus), bonus)
                : ((TileHint, Tile)?)null;
        }

        public ILottery CreateNext()
            => new InsanityLottery(heuristic.Next());
    }
}
