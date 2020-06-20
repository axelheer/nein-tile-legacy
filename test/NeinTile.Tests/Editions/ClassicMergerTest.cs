using Xunit;

namespace NeinTile.Editions.Tests
{
    public class ClassicMergerTest
    {
        public static TheoryData CanMergeData
            => new TheoryData<Tile, Tile, bool>()
            {
                { new Tile(0, 0), new Tile(0, 0), false },
                { new Tile(0, 0), new Tile(1, 0), false },
                { new Tile(1, 0), new Tile(0, 0), true },

                { new Tile(1, 0), new Tile(1, 0), false },
                { new Tile(1, 0), new Tile(2, 0), true },
                { new Tile(2, 0), new Tile(1, 0), true },
                { new Tile(2, 0), new Tile(2, 0), false },

                { new Tile(3, 3), new Tile(3, 3), true },
                { new Tile(3, 3), new Tile(6, 9), false }
            };

        [Theory]
        [MemberData(nameof(CanMergeData))]
        public void ShouldCanMerge(Tile source, Tile target, bool expected)
        {
            var subject = new ClassicMerger();

            var actual = subject.CanMerge(source, target);

            Assert.Equal(expected, actual);
        }

        public static TheoryData MergeData
            => new TheoryData<Tile, Tile, Tile>()
            {
                { new Tile(1, 0), new Tile(0, 0), new Tile(1, 0) },
                { new Tile(1, 0), new Tile(2, 0), new Tile(3, 3) },
                { new Tile(2, 0), new Tile(1, 0), new Tile(3, 3) },
                { new Tile(3, 3), new Tile(3, 3), new Tile(6, 9) }
            };

        [Theory]
        [MemberData(nameof(MergeData))]
        public void ShouldMerge(Tile source, Tile target, Tile expected)
        {
            var subject = new ClassicMerger();

            var actual = subject.Merge(source, target);

            Assert.Equal(expected, actual);
        }
    }
}
