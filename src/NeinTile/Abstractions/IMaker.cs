namespace NeinTile.Abstractions
{
    public interface IMaker
    {
        IDealer MakeDealer();

        ILottery MakeLottery();

        IMerger MakeMerger();

        IMixer MakeMixer();
    }
}
