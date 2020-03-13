using System;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaLottery : ITilesAreaLottery
    {
        public Func<ITilesAreaLottery> OnDraw { get; }
            = () => new FakeTilesAreaLottery();

        public ITilesAreaLottery Draw()
            => OnDraw();

        public Func<MoveMarking[], MoveMarking> OnPick { get; }
            = _ => MoveMarking.Empty;

        public MoveMarking Pick(MoveMarking[] markings)
            => OnPick(markings);
    }
}
