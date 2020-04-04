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
            for (var index = 0; index < markings.Length - 1; index++)
            {
                var next = random.Next(index, markings.Length);
                var temp = markings[index];
                markings[index] = markings[next];
                markings[next] = temp;
            }

            return Slice(markings, (markings.Length + 3) / 4);
        }

#if NETSTANDARD2_0

        private MoveMarking[] Slice(MoveMarking[] markings, int length)
        {
            moves = new MoveMarking[length];
            Array.Copy(markings, moves, moves.Length);
            return moves;
        }

#else

        private MoveMarking[] Slice(MoveMarking[] markings, int length)
            => moves = markings[..length];

#endif

        public ITilesAreaLottery CreateNext()
            => new DefaultTilesAreaLottery(moves, heuristic.CreateNext());
    }
}
