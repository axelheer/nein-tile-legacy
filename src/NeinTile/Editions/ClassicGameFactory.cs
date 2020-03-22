using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class ClassicGameFactory : GameFactory
    {
        public override ITilesAreaLottery CreateAreaLottery(GameOptions options)
            => new DefaultTilesAreaLottery();

        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new ClassicTilesAreaMerger();

        public override ITilesAreaMixer CreateAreaMixer(GameOptions options)
            => new DefaultTilesAreaMixer(options);

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new ClassicTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new ClassicTilesDeckMixer();
    }
}
