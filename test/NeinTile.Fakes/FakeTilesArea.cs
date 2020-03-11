using System;

namespace NeinTile.Fakes
{
    public class FakeTilesArea : TilesArea
    {
        public FakeTilesArea()
            : base (new TileInfo[0, 0, 0])
        {
        }

        public Func<MoveDirection, bool> OnCanMove { get; set; } = _ => false;

        public override bool CanMove(MoveDirection direction)
        {
            return OnCanMove(direction);
        }

        public Func<MoveDirection, TileInfo, TilesArea> OnMove { get; set; } = (_, __) => new FakeTilesArea();

        public override TilesArea Move(MoveDirection direction, TileInfo nextTile)
        {
            return OnMove(direction, nextTile);
        }
    }
}
