using System;
using System.Linq;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DefaultTilesAreaLottery : ITilesAreaLottery
    {
        private MoveMarking move;
        private readonly MoveMarking lastMove;
        private readonly DeterministicHeuristic heuristic;

        public DefaultTilesAreaLottery()
            : this(MoveMarking.Empty, DeterministicHeuristic.CreateNew())
        {
        }

        private DefaultTilesAreaLottery(MoveMarking lastMove, DeterministicHeuristic heuristic)
        {
            this.lastMove = lastMove;
            this.heuristic = heuristic;
        }

        public MoveMarking Draw(MoveMarking[] markings)
        {
            if (markings is null)
                throw new ArgumentNullException(nameof(markings));

            if (markings.Contains(lastMove))
            {
                move = lastMove;
                return move;
            }

            var random = heuristic.Next();
            var next = random.Next(markings.Length);
            move = markings[next];
            return move;
        }

        public ITilesAreaLottery CreateNext()
            => new DefaultTilesAreaLottery(move, heuristic.CreateNext());
    }
}
