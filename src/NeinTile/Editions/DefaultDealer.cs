using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class DefaultDealer : IDealer
    {
        private readonly DeterministicHeuristic heuristic;
        private readonly int ratio;

        public DefaultDealer()
            : this(DeterministicHeuristic.Create(), 2)
        {
        }

        private DefaultDealer(DeterministicHeuristic heuristic, int ratio)
        {
            this.heuristic = heuristic;
            this.ratio = ratio;
        }

        public TileIndex[] Part(TileIndex[] indices)
        {
            if (indices is null)
                throw new ArgumentNullException(nameof(indices));

            var random = heuristic.Roll();
            var range = 0..Math.Max(1, indices.Length / ratio);
            return indices.Shuffle(random)[range];
        }

        public IDealer CreateNext()
            => new DefaultDealer(heuristic.CreateNext(), 4);
    }
}
