using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class InsanityGameFactory : GameFactory
    {
        protected override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new InsanityTilesAreaMerger();

        protected override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new InsanityTilesDeckLottery();

        protected override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new InsanityTilesDeckMixer();
    }
}
