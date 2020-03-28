using Xunit;

namespace NeinTile.Editions.Tests
{
    public class InsanityTilesAreaMergerTest
    {
        public static TheoryData CanMergeData
            => new TheoryData<TileInfo, TileInfo, bool>()
            {
                { new TileInfo(-3, 0), new TileInfo(-6, 0), false },
                { new TileInfo(-3, 0), new TileInfo(-3, 0), true },
                { new TileInfo(-2, 0), new TileInfo(-2, 0), false },
                { new TileInfo(-2, 0), new TileInfo(-1, 0), true },
                { new TileInfo(-1, 0), new TileInfo(-2, 0), true },
                { new TileInfo(-1, 0), new TileInfo(-1, 0), false },
                { new TileInfo(0, 0), new TileInfo(0, 0), true },
                { new TileInfo(1, 0), new TileInfo(1, 0), false },
                { new TileInfo(1, 0), new TileInfo(2, 0), true },
                { new TileInfo(2, 0), new TileInfo(1, 0), true },
                { new TileInfo(2, 0), new TileInfo(2, 0), false },
                { new TileInfo(3, 0), new TileInfo(3, 0), true },
                { new TileInfo(3, 0), new TileInfo(6, 0), false }
            };

        [Theory]
        [MemberData(nameof(CanMergeData))]
        public void ShouldCanMerge(TileInfo source, TileInfo target, bool expected)
        {
            var subject = new InsanityTilesAreaMerger();

            var actual = subject.CanMerge(source, target);

            Assert.Equal(expected, actual);
        }

        public static TheoryData MergeData
            => new TheoryData<TileInfo, TileInfo, TileInfo>()
            {
                { new TileInfo(-6, 6), new TileInfo(-6, 6), new TileInfo(-12, 12) },
                { new TileInfo(-3, 3), new TileInfo(-3, 3), new TileInfo(-6, 6) },
                { new TileInfo(-2, 0), new TileInfo(-1, 0), new TileInfo(-3, 3) },
                { new TileInfo(-1, 0), new TileInfo(-2, 0), new TileInfo(-3, 3) },
                { new TileInfo(0, 100), new TileInfo(0, 50), new TileInfo(0, 100) },
                { new TileInfo(1, 0), new TileInfo(2, 0), new TileInfo(3, 3) },
                { new TileInfo(2, 0), new TileInfo(1, 0), new TileInfo(3, 3) },
                { new TileInfo(3, 3), new TileInfo(3, 3), new TileInfo(6, 9) },
                { new TileInfo(6, 9), new TileInfo(6, 9), new TileInfo(12, 27) }
            };

        [Theory]
        [MemberData(nameof(MergeData))]
        public void ShouldMerge(TileInfo source, TileInfo target, TileInfo expected)
        {
            var subject = new InsanityTilesAreaMerger();

            var actual = subject.Merge(source, target, out var remainder);

            Assert.Equal(expected, actual);
            Assert.Equal(TileInfo.Empty, remainder);
        }
    }
}
