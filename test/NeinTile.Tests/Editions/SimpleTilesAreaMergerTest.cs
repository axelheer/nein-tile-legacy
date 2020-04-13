using Xunit;

namespace NeinTile.Editions.Tests
{
    public class SimpleTilesAreaMergerTest
    {
        public static TheoryData CanMergeData
            => new TheoryData<TileInfo, TileInfo, bool>()
            {
                { new TileInfo(0, 0), new TileInfo(0, 0), false },
                { new TileInfo(0, 0), new TileInfo(2, 2), false },
                { new TileInfo(2, 2), new TileInfo(0, 0), true },

                { new TileInfo(2, 2), new TileInfo(4, 6), false },
                { new TileInfo(2, 2), new TileInfo(2, 2), true },
                { new TileInfo(4, 6), new TileInfo(4, 6), true },
                { new TileInfo(4, 6), new TileInfo(2, 2), false }
            };

        [Theory]
        [MemberData(nameof(CanMergeData))]
        public void ShouldCanMerge(TileInfo source, TileInfo target, bool expected)
        {
            var subject = new SimpleTilesAreaMerger();

            var actual = subject.CanMerge(source, target);

            Assert.Equal(expected, actual);
        }

        public static TheoryData MergeData
            => new TheoryData<TileInfo, TileInfo, TileInfo>()
            {
                { new TileInfo(2, 2), new TileInfo(0, 0), new TileInfo(2, 2) },
                { new TileInfo(2, 2), new TileInfo(2, 2), new TileInfo(4, 6) },
                { new TileInfo(4, 6), new TileInfo(4, 6), new TileInfo(8, 18) }
            };

        [Theory]
        [MemberData(nameof(MergeData))]
        public void ShouldMerge(TileInfo source, TileInfo target, TileInfo expected)
        {
            var subject = new SimpleTilesAreaMerger();

            var actual = subject.Merge(source, target, out var remainder);

            Assert.Equal(expected, actual);
            Assert.Equal(TileInfo.Empty, remainder);
        }
    }
}
