namespace NeinTile
{
    public interface ITilesAreaLottery
    {
        MoveMarking Draw(MoveMarking[] markings);

        ITilesAreaLottery CreateNext();
    }
}
