using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class ClassicMaker : IMaker
    {
        public IDealer MakeDealer()
            => new DefaultDealer();

        public ILottery MakeLottery()
            => new ClassicLottery();

        public IMerger MakeMerger()
            => new ClassicMerger();

        public IMixer MakeMixer()
            => new ClassicMixer();
    }
}
