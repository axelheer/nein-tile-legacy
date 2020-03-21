namespace NeinTile.Abstractions
{
    public interface ITilesDeckMixer
    {
        TileInfo[] Shuffle();

        ITilesDeckMixer CreateNext();
    }
}
