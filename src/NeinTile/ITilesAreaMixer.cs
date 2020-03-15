namespace NeinTile
{
    public interface ITilesAreaMixer
    {
        TileInfo[,,] Shuffle();

        TilesDeck CreateDeck();
    }
}
