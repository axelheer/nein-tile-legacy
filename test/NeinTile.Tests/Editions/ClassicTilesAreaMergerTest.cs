using Xunit;

namespace NeinTile.Editions.Tests
{
    public class ClassicTilesAreaMergerTest
    {
        public static TheoryData CanMergeData
            => new TheoryData<TileInfo, TileInfo, bool>()
            {
                { new TileInfo(0, 0), new TileInfo(0, 0), false },
                { new TileInfo(0, 0), new TileInfo(1, 0), false },
                { new TileInfo(1, 0), new TileInfo(0, 0), true },

                { new TileInfo(1, 0), new TileInfo(1, 0), false },
                { new TileInfo(1, 0), new TileInfo(2, 0), true },
                { new TileInfo(2, 0), new TileInfo(1, 0), true },
                { new TileInfo(2, 0), new TileInfo(2, 0), false },

                { new TileInfo(3, 3), new TileInfo(3, 3), true },
                { new TileInfo(3, 3), new TileInfo(6, 9), false }
            };

        [Theory]
        [MemberData(nameof(CanMergeData))]
        public void ShouldCanMerge(TileInfo source, TileInfo target, bool expected)
        {
            var subject = new ClassicTilesAreaMerger();

            var actual = subject.CanMerge(source, target);

            Assert.Equal(expected, actual);
        }

        public static TheoryData MergeData
            => new TheoryData<TileInfo, TileInfo, TileInfo>()
            {
                { new TileInfo(1, 0), new TileInfo(0, 0), new TileInfo(1, 0) },
                { new TileInfo(1, 0), new TileInfo(2, 0), new TileInfo(3, 3) },
                { new TileInfo(2, 0), new TileInfo(1, 0), new TileInfo(3, 3) },
                { new TileInfo(3, 3), new TileInfo(3, 3), new TileInfo(6, 9) }
            };

        [Theory]
        [MemberData(nameof(MergeData))]
        public void ShouldMerge(TileInfo source, TileInfo target, TileInfo expected)
        {
            var subject = new ClassicTilesAreaMerger();

            var actual = subject.Merge(source, target, out var remainder);

            Assert.Equal(expected, actual);
            Assert.Equal(TileInfo.Empty, remainder);
        }
    }
}
