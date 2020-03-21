using NeinTile.Abstractions;

namespace NeinTile.Rules
{
    public class ClassicGameFactory : GameFactory
    {
        public override ITilesAreaLottery CreateAreaLottery(GameOptions options)
            => new ClassicTilesAreaLottery();

        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new ClassicTilesAreaMerger();

        public override ITilesAreaMixer CreateAreaMixer(GameOptions options)
            => new ClassicTilesAreaMixer(options);

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new ClassicTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new ClassicTilesDeckMixer();
    }
}
