using System;

namespace NeinTile.Fakes
{
    public class FakeTilesAreaLottery : ITilesAreaLottery
    {
        public Func<MoveMarking[], MoveMarking> OnDraw { get; set; }
            = _ => MoveMarking.Empty;

        public MoveMarking Draw(MoveMarking[] markings)
            => OnDraw(markings);

        public Func<ITilesAreaLottery> OnCreateNext { get; set; }
            = () => new FakeTilesAreaLottery();

        public ITilesAreaLottery CreateNext()
            => OnCreateNext();
    }
}
