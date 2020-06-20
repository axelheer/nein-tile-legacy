namespace NeinTile.Abstractions
{
    public interface ILottery
    {
        (TileHint, Tile)? Draw(long maxValue);

        ILottery CreateNext();
    }
}
