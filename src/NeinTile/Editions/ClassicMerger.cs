using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class ClassicMerger : IMerger
    {
        public bool CanMerge(Tile source, Tile target)
        {
            return (source.Value, target.Value) switch
            {
                (0, _) => false,
                (_, 0) => true,
                (1, 1) => false,
                (1, 2) => true,
                (2, 1) => true,
                (2, 2) => false,
                var (s, t) => s == t
            };
        }

        public Tile Merge(Tile source, Tile target)
        {
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                ((1, 0), (2, 0)) => new Tile(3, 3),
                ((2, 0), (1, 0)) => new Tile(3, 3),
                var ((v1, s1), (v2, s2)) => new Tile(v1 + v2, s1 + s2 + s2)
            };
        }
    }
}
