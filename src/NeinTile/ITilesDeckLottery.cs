namespace NeinTile
{
    public interface ITilesDeckLottery
    {
        TileInfo Bonus { get; }

        TileSample Sample { get; }

        ITilesDeckLottery Draw();
    }
}
