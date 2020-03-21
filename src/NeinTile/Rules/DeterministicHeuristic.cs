using System;

namespace NeinTile
{
    internal struct DeterministicHeuristic
    {
        private static readonly Random initRandom = new Random();
        private static readonly object randomLock = new object();

        public static DeterministicHeuristic CreateNew()
        {
            lock (randomLock)
            {
                return new DeterministicHeuristic(initRandom.Next());
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

        public Random Next()
            => new Random(seed);

        public DeterministicHeuristic CreateNext()
            => new DeterministicHeuristic(next);
    }
}
