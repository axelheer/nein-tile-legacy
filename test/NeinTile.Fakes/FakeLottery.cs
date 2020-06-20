using System;
using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public class FakeLottery : ILottery
    {
        public Func<long, (TileHint, Tile)?> OnDraw { get; set; }
            = _ => null;

        public (TileHint, Tile)? Draw(long maxValue)
            => OnDraw(maxValue);

        public Func<ILottery> OnCreateNext { get; set; }
            = () => new FakeLottery();

        public ILottery CreateNext()
            => OnCreateNext();
    }
}
