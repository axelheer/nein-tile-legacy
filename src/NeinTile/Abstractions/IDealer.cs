namespace NeinTile.Abstractions
{
    public interface IDealer
    {
        TileIndex[] Part(TileIndex[] indices);

        IDealer CreateNext();
    }
}
