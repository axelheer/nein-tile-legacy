namespace NeinTile.Abstractions
{
    public interface IMerger
    {
        bool CanMerge(Tile source, Tile target);

        Tile Merge(Tile source, Tile target);
    }
}
