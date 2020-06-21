using System;

namespace NeinTile
{
    [Serializable]
    internal struct DeterministicHeuristic
    {
        private static readonly Random InitRandom = new Random();
        private static readonly object RandomLock = new object();

        public static DeterministicHeuristic Create()
        {
            lock (RandomLock)
            {
                return new DeterministicHeuristic(InitRandom.Next());
            }
        }

        private readonly int seed;
        private readonly int next;

        private DeterministicHeuristic(int parent)
        {
            var random = new Random(parent);
            seed = random.Next();
            next = random.Next();
        }

        public Random Roll()
            => new Random(seed);

        public DeterministicHeuristic CreateNext()
            => new DeterministicHeuristic(next);
    }
}
