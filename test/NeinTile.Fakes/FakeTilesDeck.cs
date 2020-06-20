using NeinTile.Abstractions;

namespace NeinTile.Fakes
{
    public static class FakeTilesDeck
    {
        public static TilesDeck Create(IMixer? mixer = null, ILottery? lottery = null)
            => new TilesDeck(mixer ?? new FakeMixer(), lottery ?? new FakeLottery());
    }
}
