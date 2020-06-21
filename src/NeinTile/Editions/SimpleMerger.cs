using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class SimpleMerger : IMerger
    {
        public bool CanMerge(Tile source, Tile target)
        {
            return (source.Value, target.Value) switch
            {
                (0, _) => false,
                (_, 0) => true,
                var (s, t) => s == t
            };
        }

        public Tile Merge(Tile source, Tile target)
        {
            return (source, target) switch
            {
                (_, (0, 0)) => source,
                var ((v1, s1), (v2, s2)) => new Tile(v1 + v2, s1 + s2 + v1 + v2)
            };
        }
    }
}
