
namespace NeinTile
{
    public interface ITilesDeckMixer
    {
        TileInfo[] Tiles { get; }

        ITilesDeckMixer Shuffle();
    }
}
