namespace NeinTile
{
    public interface ITilesAreaMixer
    {
        TileInfo[,,] Tiles { get; }

        ITilesAreaMixer Shuffle();
    }
}
