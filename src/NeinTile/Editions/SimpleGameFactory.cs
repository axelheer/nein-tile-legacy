using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class SimpleGameFactory : GameFactory
    {
        public override ITilesAreaLottery CreateAreaLottery(GameOptions options)
            => new DefaultTilesAreaLottery();

        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new SimpleTilesAreaMerger();

        public override ITilesAreaMixer CreateAreaMixer(GameOptions options)
            => new DefaultTilesAreaMixer(options);

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new SimpleTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new SimpleTilesDeckMixer();
    }
}
