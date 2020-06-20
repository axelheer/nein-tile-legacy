using System;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    [Serializable]
    public sealed class SimpleMixer : IMixer
    {
        private readonly Tile[] tiles = new[]
        {
            new Tile(2, 2)
        };

        public Tile[] Mix()
            => tiles;

        public IMixer CreateNext()
            => this;
    }
}
