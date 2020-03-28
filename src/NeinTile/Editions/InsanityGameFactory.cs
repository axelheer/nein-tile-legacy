using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class InsanityGameFactory : GameFactory
    {
        public override ITilesAreaLottery CreateAreaLottery(GameOptions options)
            => new DefaultTilesAreaLottery();

        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new InsanityTilesAreaMerger();

        public override ITilesAreaMixer CreateAreaMixer(GameOptions options)
            => new DefaultTilesAreaMixer(options);

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new InsanityTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new InsanityTilesDeckMixer();
    }
}
