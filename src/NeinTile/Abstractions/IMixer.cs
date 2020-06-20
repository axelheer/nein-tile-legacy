namespace NeinTile.Abstractions
{
    public interface IMixer
    {
        Tile[] Mix();

        IMixer CreateNext();
    }
}
