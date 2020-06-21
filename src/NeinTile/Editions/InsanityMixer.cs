using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class InsanityMixer : IMixer
    {
        private readonly DeterministicHeuristic heuristic;

        public InsanityMixer()
            : this(DeterministicHeuristic.Create())
        {
        }

        private InsanityMixer(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public Tile[] Mix()
        {
            var random = heuristic.Roll();
            var result = new[]
            {
                new Tile(-3, 3),
                new Tile(-3, 3),

                new Tile(-2, 2),
                new Tile(-2, 2),

                new Tile(-1, 1),
                new Tile(-1, 1),

                new Tile(1, 1),
                new Tile(1, 1),
                new Tile(1, 1),
                new Tile(1, 1),

                new Tile(2, 2),
                new Tile(2, 2),
                new Tile(2, 2),
                new Tile(2, 2),

                new Tile(3, 3),
                new Tile(3, 3),
                new Tile(3, 3),
                new Tile(3, 3)
            };
            return result.Shuffle(random);
        }

        public IMixer CreateNext()
            => new InsanityMixer(heuristic.CreateNext());
    }
}
