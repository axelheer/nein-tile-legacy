using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class SimpleGameFactory : GameFactory
    {
        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new SimpleTilesAreaMerger();

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new SimpleTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new SimpleTilesDeckMixer();
    }
}
