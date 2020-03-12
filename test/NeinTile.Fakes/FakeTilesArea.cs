using System;

namespace NeinTile.Fakes
{
    public class FakeTilesArea : TilesArea
    {
        public FakeTilesArea()
            : base (new FakeTilesAreaMixer(), new FakeTilesAreaMerger())
        {
        }

        public Func<MoveDirection, bool> OnCanMove { get; set; }
            = _ => false;

        public override bool CanMove(MoveDirection direction)
            => OnCanMove(direction);

        public Func<MoveDirection, TileInfo, TilesArea> OnMove { get; set; }
            = (_, __) => new FakeTilesArea();

        public override TilesArea Move(MoveDirection direction, TileInfo nextTile)
            => OnMove(direction, nextTile);
    }
}