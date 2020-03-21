namespace NeinTile.Abstractions
{
    public interface ITilesAreaMerger
    {
        bool CanMerge(TileInfo source, TileInfo target);

        TileInfo Merge(TileInfo source, TileInfo target, out TileInfo remainder);
    }
}
