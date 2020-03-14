namespace NeinTile
{
    public interface ITilesDeckLottery
    {
        TileSample Draw(TileInfo[] tiles, out TileInfo bonus);

        ITilesDeckLottery CreateNext();
    }
}
