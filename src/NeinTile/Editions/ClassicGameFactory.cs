using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class ClassicGameFactory : GameFactory
    {
        public override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new ClassicTilesAreaMerger();

        public override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new ClassicTilesDeckLottery();

        public override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new ClassicTilesDeckMixer();
    }
}
