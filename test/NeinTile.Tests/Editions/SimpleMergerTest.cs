using Xunit;

namespace NeinTile.Editions.Tests
{
    public class SimpleMergerTest
    {
        public static TheoryData CanMergeData
            => new TheoryData<Tile, Tile, bool>()
            {
                { new Tile(0, 0), new Tile(0, 0), false },
                { new Tile(0, 0), new Tile(2, 2), false },
                { new Tile(2, 2), new Tile(0, 0), true },

                { new Tile(2, 2), new Tile(4, 6), false },
                { new Tile(2, 2), new Tile(2, 2), true },
                { new Tile(4, 6), new Tile(4, 6), true },
                { new Tile(4, 6), new Tile(2, 2), false }
            };

        [Theory]
        [MemberData(nameof(CanMergeData))]
        public void ShouldCanMerge(Tile source, Tile target, bool expected)
        {
            var subject = new SimpleMerger();

            var actual = subject.CanMerge(source, target);

            Assert.Equal(expected, actual);
        }

        public static TheoryData MergeData
            => new TheoryData<Tile, Tile, Tile>()
            {
                { new Tile(2, 2), new Tile(0, 0), new Tile(2, 2) },
                { new Tile(2, 2), new Tile(2, 2), new Tile(4, 8) },
                { new Tile(4, 8), new Tile(4, 8), new Tile(8, 24) }
            };

        [Theory]
        [MemberData(nameof(MergeData))]
        public void ShouldMerge(Tile source, Tile target, Tile expected)
        {
            var subject = new SimpleMerger();

            var actual = subject.Merge(source, target);

            Assert.Equal(expected, actual);
        }
    }
}
