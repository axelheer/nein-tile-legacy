namespace NeinTile.Abstractions
{
    public interface ITilesAreaMixer
    {
        TileInfo[,,] Tiles { get; }

        bool AddNext(TileInfo nextTile);
    }
}
