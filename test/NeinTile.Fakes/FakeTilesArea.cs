using System;

namespace NeinTile.Fakes
{
    public class FakeTilesArea : TilesArea
    {
        public FakeTilesArea()
            : base (new TileInfo[0, 0, 0])
        {
        }

        public FakeTilesArea(TileInfo[,,] tiles)
            : base(tiles)
        {
        }

        public Func<MoveDirection, bool> OnCanMove { get; set; }

        public override bool CanMove(MoveDirection direction)
        {
            return OnCanMove(direction);
        }

        public Func<MoveDirection, TileInfo, TilesArea> OnMove { get; set; }

        public override TilesArea Move(MoveDirection direction, TileInfo nextTile)
        {
            return OnMove(direction, nextTile);
        }
    }
}
