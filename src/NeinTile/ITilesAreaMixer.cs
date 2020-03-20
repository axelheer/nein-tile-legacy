namespace NeinTile
{
    public interface ITilesAreaMixer
    {
        bool AddNext(TileInfo nextTile);

        TileInfo[,,]? Shuffle();
    }
}
