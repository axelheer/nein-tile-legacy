using System;
using NeinTile.Abstractions;

namespace NeinTile
{
    [Serializable]
    public sealed class TilesDeck
    {
        public ILottery Lottery { get; }
        public IMixer Mixer { get; }

        public Tile Tile { get; }
        public TileHint Hint { get; }

        private readonly Tile[] stack;

        public TilesDeck(IMixer mixer, ILottery lottery)
        {
            if (mixer is null)
                throw new ArgumentNullException(nameof(mixer));
            if (lottery is null)
                throw new ArgumentNullException(nameof(lottery));

            var next = mixer.Mix();

            stack = next[1..];
            Tile = next[0];
            Hint = new TileHint(next[0]);
            Mixer = mixer.CreateNext();
            Lottery = lottery;
        }

        private TilesDeck(Tile[] stack, Tile tile, TileHint hint, IMixer mixer, ILottery lottery)
        {
            this.stack = stack;

            Tile = tile;
            Hint = hint;
            Mixer = mixer;
            Lottery = lottery;
        }

        public TilesDeck Next(long maxValue)
        {
            var bonus = Lottery.Draw(maxValue);
            if (bonus != null)
            {
                var (hint, tile) = bonus.Value;
                return new TilesDeck(stack, tile, hint, Mixer, Lottery.CreateNext());
            }
            return stack.Length != 0
                ? new TilesDeck(stack[1..], stack[0], new TileHint(stack[0]), Mixer, Lottery.CreateNext())
                : new TilesDeck(Mixer, Lottery.CreateNext());
        }
    }
}
