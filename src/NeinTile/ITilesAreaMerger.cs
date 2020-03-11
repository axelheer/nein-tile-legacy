namespace NeinTile
{
    public interface ITilesAreaMerger
    {
        bool CanMerge(TileInfo tile, TileInfo other);

        TileInfo Merge(TileInfo tile, TileInfo other);
    }
}
