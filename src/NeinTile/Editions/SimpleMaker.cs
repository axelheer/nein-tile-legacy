using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class SimpleMaker : IMaker
    {
        public IDealer MakeDealer()
            => new DefaultDealer();

        public ILottery MakeLottery()
            => new SimpleLottery();

        public IMerger MakeMerger()
            => new SimpleMerger();

        public IMixer MakeMixer()
            => new SimpleMixer();
    }
}
