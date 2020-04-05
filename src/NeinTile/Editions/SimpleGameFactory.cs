using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class SimpleGameFactory : GameFactory
    {
        protected override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new SimpleTilesAreaMerger();

        protected override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new SimpleTilesDeckLottery();

        protected override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new SimpleTilesDeckMixer();
    }
}
