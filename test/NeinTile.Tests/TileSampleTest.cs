using System;
using Xunit;

namespace NeinTile.Tests
{
    public class TileSampleTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new TileSample();

            Assert.Equal(TileSample.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.Equal(new TileInfo(1, 2), subject.First);
            Assert.Equal(new TileInfo(3, 4), subject.Second);
            Assert.Equal(new TileInfo(5, 6), subject.Third);
        }

        [Fact]
        public void ShouldBeExplicit()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(1, 2),
                new TileInfo(1, 2)
            );

            Assert.True(subject.IsExplicit);
        }

        [Fact]
        public void ShouldBeNotExplicit()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.False(subject.IsExplicit);
        }

        [Fact]
        public void ShouldCombineHashCode()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var expected = HashCode.Combine(subject.First, subject.Second, subject.Third);
            var actual = subject.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject.Equals(
                new TileSample(
                    new TileInfo(1, 2),
                    new TileInfo(3, 4),
                    new TileInfo(5, 6)
                )
            );

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject.Equals(
                new TileSample(
                    new TileInfo(1, 2),
                    new TileInfo(3, 4),
                    new TileInfo(5, -1)
                )
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject == new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject == new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, -1)
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject != new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            var actual = subject != new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, -1)
            );

            Assert.True(actual);
        }
    }
}
