using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class ClassicTilesDeckMixer : ITilesDeckMixer
    {
        private readonly DeterministicHeuristic heuristic;

        public ClassicTilesDeckMixer()
            : this(DeterministicHeuristic.CreateNew())
        {
        }

        private ClassicTilesDeckMixer(DeterministicHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public TileInfo[] Shuffle()
        {
            var random = heuristic.Next();
            var result = new[]
            {
                new TileInfo(1, 0),
                new TileInfo(1, 0),
                new TileInfo(1, 0),
                new TileInfo(1, 0),

                new TileInfo(2, 0),
                new TileInfo(2, 0),
                new TileInfo(2, 0),
                new TileInfo(2, 0),

                new TileInfo(3, 3),
                new TileInfo(3, 3),
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
            => new ClassicTilesDeckMixer(heuristic.CreateNext());
    }
}
