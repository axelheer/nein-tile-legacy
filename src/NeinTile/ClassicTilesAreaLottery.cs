using System;
using System.Linq;

namespace NeinTile
{
    public sealed class ClassicTilesAreaLottery : ITilesAreaLottery
    {
        private MoveMarking move;
        private readonly MoveMarking lastMove;
        private readonly DeterministicHeuristic heuristic;

        public ClassicTilesAreaLottery()
            : this(MoveMarking.Empty, DeterministicHeuristic.CreateNew())
        {
        }

        private ClassicTilesAreaLottery(MoveMarking lastMove, DeterministicHeuristic heuristic)
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
            => new ClassicTilesAreaLottery(move, heuristic.CreateNext());
    }
}
