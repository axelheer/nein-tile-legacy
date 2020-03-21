namespace NeinTile.Abstractions
{
    public interface ITilesAreaLottery
    {
        MoveMarking Draw(MoveMarking[] markings);

        ITilesAreaLottery CreateNext();
    }
}
