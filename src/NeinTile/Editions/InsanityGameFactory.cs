using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class InsanityGameFactory : GameFactory
    {
        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new InsanityTilesAreaMerger();

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new InsanityTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new InsanityTilesDeckMixer();
    }
}
