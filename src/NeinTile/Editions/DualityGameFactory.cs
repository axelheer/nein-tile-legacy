using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DualityGameFactory : GameFactory
    {
        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new DualityTilesAreaMerger();

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new DualityTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new DualityTilesDeckMixer();
    }
}
