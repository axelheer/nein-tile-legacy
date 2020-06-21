using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class ClassicMixer : IMixer
    {
        private readonly DeterministicHeuristic heuristic;

        public ClassicMixer()
            : this(DeterministicHeuristic.Create())
        {
        }

        private ClassicMixer(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public Tile[] Mix()
        {
            var random = heuristic.Roll();
            var result = new[]
            {
                new Tile(1, 0),
                new Tile(1, 0),
                new Tile(1, 0),
                new Tile(1, 0),

                new Tile(2, 0),
                new Tile(2, 0),
                new Tile(2, 0),
                new Tile(2, 0),

                new Tile(3, 3),
                new Tile(3, 3),
                new Tile(3, 3),
                new Tile(3, 3)
            };
            return result.Shuffle(random);
        }

        public IMixer CreateNext()
            => new ClassicMixer(heuristic.CreateNext());
    }
}
