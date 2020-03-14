
namespace NeinTile
{
    public interface ITilesDeckMixer
    {
        TileInfo[] Shuffle();

        ITilesDeckMixer CreateNext();
    }
}
