using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class InsanityMaker : IMaker
    {
        public IDealer MakeDealer()
            => new DefaultDealer();

        public ILottery MakeLottery()
            => new InsanityLottery();

        public IMerger MakeMerger()
            => new InsanityMerger();

        public IMixer MakeMixer()
            => new InsanityMixer();
    }
}
