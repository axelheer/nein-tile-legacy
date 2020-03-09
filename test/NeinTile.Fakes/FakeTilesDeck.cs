using System;

namespace NeinTile.Fakes
{
    public class FakeTilesDeck : TilesDeck
    {
        public FakeTilesDeck()
            : base(new TileInfo[0])
        {
        }

        public FakeTilesDeck(TileInfo[] tiles)
            : base(tiles)
        {
        }

        public Func<(TilesDeck, TileInfo)> OnDraw { get; set; }

        public override TilesDeck Draw(out TileInfo tile)
        {
            TilesDeck result;
            (result, tile) = OnDraw();
            return result;
        }

        public Func<TileInfo[]> OnPreview { get; set; }

        public override TileInfo[] Preview()
        {
            return OnPreview();
        }
    }
}
