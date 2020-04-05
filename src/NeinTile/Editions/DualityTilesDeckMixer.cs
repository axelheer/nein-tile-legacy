using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DualityTilesDeckMixer : ITilesDeckMixer
    {
        private readonly DeterministicHeuristic heuristic;

        public DualityTilesDeckMixer()
            : this(DeterministicHeuristic.CreateNew())
        {
        }

        private DualityTilesDeckMixer(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public TileInfo[] Shuffle()
        {
            var random = heuristic.Next();
            var result = new[]
            {
                new TileInfo(-2, 2),
                new TileInfo(-2, 2),

                new TileInfo(2, 2),
                new TileInfo(2, 2)
            };
            for (var index = 0; index < result.Length - 1; index++)
            {
                var next = random.Next(index, result.Length);
                var temp = result[index];
                result[index] = result[next];
                result[next] = temp;
            }
            return result;
        }

        public ITilesDeckMixer CreateNext()
            => new DualityTilesDeckMixer(heuristic.CreateNext());
    }
}
