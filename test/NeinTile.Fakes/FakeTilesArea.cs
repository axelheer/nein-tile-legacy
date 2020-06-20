using NeinTile.Abstractions;
using NeinTile.Editions;

namespace NeinTile.Fakes
{
    public static class FakeTilesArea
    {
        public static TilesArea Create(Tiles? tiles = null, IDealer? dealer = null, IMerger? merger = null)
            => new TilesArea(tiles ?? new Tiles(4, 4, 1), dealer ?? new DefaultDealer(), merger ?? new FakeMerger());
    }
}
