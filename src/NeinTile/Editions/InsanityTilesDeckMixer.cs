using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class InsanityTilesDeckMixer : ITilesDeckMixer
    {
        private readonly DeterministicHeuristic heuristic;

        public InsanityTilesDeckMixer()
            : this(DeterministicHeuristic.CreateNew())
        {
        }

        private InsanityTilesDeckMixer(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public TileInfo[] Shuffle()
        {
            var random = heuristic.Next();
            var result = new[]
            {
                new TileInfo(-3, 3),
                new TileInfo(-3, 3),

                new TileInfo(-2, 0),
                new TileInfo(-2, 0),

                new TileInfo(-1, 0),
                new TileInfo(-1, 0),

                new TileInfo(1, 0),
                new TileInfo(1, 0),

                new TileInfo(2, 0),
                new TileInfo(2, 0),

                new TileInfo(3, 3),
                new TileInfo(3, 3)
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
            => new InsanityTilesDeckMixer(heuristic.CreateNext());
    }
}
