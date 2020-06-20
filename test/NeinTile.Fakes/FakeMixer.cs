using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeMixer : IMixer
    {
        public Func<Tile[]> OnMix { get; set; }
            = () => new[] { Tile.Empty };

        public Tile[] Mix()
            => OnMix();

        public Func<IMixer> OnCreateNext { get; set; }
            = () => new FakeMixer();

        public IMixer CreateNext()
            => OnCreateNext();
    }
}
