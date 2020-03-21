namespace NeinTile.Abstractions
{
    public interface ITilesDeckLottery
    {
        TileSample Draw(out TileInfo bonus);

        ITilesDeckLottery CreateNext(TilesArea? area = null);
    }
}
