using Xunit;

namespace NeinTile.Editions.Tests
{
    public class InsanityTilesAreaMergerTest
    {
        public static TheoryData CanMergeData
            => new TheoryData<Tile, Tile, bool>()
            {
                { new Tile(0, 100), new Tile(0, 50), true },
                { new Tile(0, 50), new Tile(0, 100), true },
                { new Tile(0, 0), new Tile(0, 0), false },
                { new Tile(0, 0), new Tile(1, 0), false },
                { new Tile(1, 0), new Tile(0, 0), true },
                { new Tile(0, 0), new Tile(-1, 0), false },
                { new Tile(-1, 0), new Tile(0, 0), true },

                { new Tile(1, 0), new Tile(1, 0), false },
                { new Tile(1, 0), new Tile(2, 0), true },
                { new Tile(2, 0), new Tile(1, 0), true },
                { new Tile(2, 0), new Tile(2, 0), false },
                { new Tile(-1, 0), new Tile(-1, 0), false },
                { new Tile(-1, 0), new Tile(-2, 0), true },
                { new Tile(-2, 0), new Tile(-1, 0), true },
                { new Tile(-2, 0), new Tile(-2, 0), false },

                { new Tile(3, 3), new Tile(3, 3), true },
                { new Tile(3, 3), new Tile(6, 9), false },
                { new Tile(-3, 3), new Tile(-3, 3), true },
                { new Tile(-3, 3), new Tile(-6, 6), false },

                { new Tile(1, 0), new Tile(-2, 0), false },
                { new Tile(2, 0), new Tile(-1, 0), false },
                { new Tile(-1, 0), new Tile(2, 0), false },
                { new Tile(-2, 0), new Tile(1, 0), false },
                { new Tile(3, 3), new Tile(-3, 3), true },
                { new Tile(-3, 3), new Tile(3, 3), true },
            };

        [Theory]
        [MemberData(nameof(CanMergeData))]
        public void ShouldCanMerge(Tile source, Tile target, bool expected)
        {
            var subject = new InsanityMerger();

            var actual = subject.CanMerge(source, target);

            Assert.Equal(expected, actual);
        }

        public static TheoryData MergeData
            => new TheoryData<Tile, Tile, Tile>()
            {
                { new Tile(0, 100), new Tile(0, 50), new Tile(0, 100) },
                { new Tile(0, 50), new Tile(0, 100), new Tile(0, 100) },
                { new Tile(1, 0), new Tile(0, 0), new Tile(1, 0) },
                { new Tile(-1, 0), new Tile(0, 0), new Tile(-1, 0) },
                { new Tile(1, 0), new Tile(2, 0), new Tile(3, 3) },
                { new Tile(2, 0), new Tile(1, 0), new Tile(3, 3) },
                { new Tile(-1, 0), new Tile(-2, 0), new Tile(-3, 3) },
                { new Tile(-2, 0), new Tile(-1, 0), new Tile(-3, 3) },
                { new Tile(3, 3), new Tile(3, 3), new Tile(6, 9) },
                { new Tile(-3, 3), new Tile(-3, 3), new Tile(-6, 6) }
            };

        [Theory]
        [MemberData(nameof(MergeData))]
        public void ShouldMerge(Tile source, Tile target, Tile expected)
        {
            var subject = new InsanityMerger();

            var actual = subject.Merge(source, target);

            Assert.Equal(expected, actual);
        }
    }
}
