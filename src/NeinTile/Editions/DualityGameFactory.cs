using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DualityGameFactory : GameFactory
    {
        protected override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new DualityTilesAreaMerger();

        protected override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new DualityTilesDeckLottery();

        protected override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new DualityTilesDeckMixer();
    }
}
