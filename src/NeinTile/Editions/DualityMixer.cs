using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class DualityMixer : IMixer
    {
        private readonly DeterministicHeuristic heuristic;

        public DualityMixer()
            : this(DeterministicHeuristic.Create())
        {
        }

        private DualityMixer(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public Tile[] Mix()
        {
            var random = heuristic.Roll();
            var result = new[]
            {
                new Tile(-2, 2),

                new Tile(2, 2),
                new Tile(2, 2)
            };
            return result.Shuffle(random);
        }

        public IMixer CreateNext()
            => new DualityMixer(heuristic.Next());
    }
}
