namespace NeinTile
{
    public interface ITilesAreaLottery
    {
        MoveMarking Pick(MoveMarking[] markings);

        ITilesAreaLottery Draw();
    }
}
