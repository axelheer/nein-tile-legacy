using System;
using System.Linq;
using NeinTile.Abstractions;

namespace NeinTile.Editions
{
    public sealed class DefaultTilesAreaLottery : ITilesAreaLottery
    {
        private MoveMarking[] moves;
        private readonly MoveMarking[] lastMoves;
        private readonly DeterministicHeuristic heuristic;

        public DefaultTilesAreaLottery()
            : this(Array.Empty<MoveMarking>(), DeterministicHeuristic.CreateNew())
        {
        }

        private DefaultTilesAreaLottery(MoveMarking[] lastMoves, DeterministicHeuristic heuristic)
        {
            this.lastMoves = lastMoves;
            this.heuristic = heuristic;

            moves = Array.Empty<MoveMarking>();
        }

        public MoveMarking[] Draw(MoveMarking[] markings)
        {
            if (markings is null)
                throw new ArgumentNullException(nameof(markings));

            if (lastMoves.Any() && lastMoves.All(move => markings.Contains(move)))
            {
                moves = lastMoves;
                return moves;
            }

            var random = heuristic.Next();

            moves = new MoveMarking[(markings.Length + 3) / 4];
            for (var index = 0; index < moves.Length; index++)
            {
                var next = markings[random.Next(markings.Length)];
                while (moves.Contains(next))
                    next = markings[random.Next(markings.Length)];
                moves[index] = next;
            }
            return moves;
        }

        public ITilesAreaLottery CreateNext()
            => new DefaultTilesAreaLottery(moves, heuristic.CreateNext());
    }
}
