namespace NeinTile.Abstractions
{
    public interface ITilesAreaMixer
    {
        bool AddNext(TileInfo nextTile);

        TileInfo[,,]? Shuffle();
    }
}
