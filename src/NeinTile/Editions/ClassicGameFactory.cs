using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class ClassicGameFactory : GameFactory
    {
        protected override ITilesAreaMerger CreateAreaMerger(GameOptions options)
            => new ClassicTilesAreaMerger();

        protected override ITilesDeckLottery CreateDeckLottery(GameOptions options)
            => new ClassicTilesDeckLottery();

        protected override ITilesDeckMixer CreateDeckMixer(GameOptions options)
            => new ClassicTilesDeckMixer();
    }
}
