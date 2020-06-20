using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DualityMaker : IMaker
    {
        public IDealer MakeDealer()
            => new DefaultDealer();

        public ILottery MakeLottery()
            => new DualityLottery();

        public IMerger MakeMerger()
            => new DualityMerger();

        public IMixer MakeMixer()
            => new DualityMixer();
    }
}
