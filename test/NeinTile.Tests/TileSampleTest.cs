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
            var (first, second, third) = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.Equal(new TileInfo(1, 2), first);
            Assert.Equal(new TileInfo(3, 4), second);
            Assert.Equal(new TileInfo(5, 6), third);
        }

        [Fact]
        public void ShouldBeSingle()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(1, 2),
                new TileInfo(1, 2)
            );

            Assert.True(subject.IsSingle);
        }

        [Fact]
        public void ShouldBeNotSingle()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.False(subject.IsSingle);
        }

        [Fact]
        public void ShouldBeEither()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(3, 4)
            );

            Assert.True(subject.IsEither);
        }

        [Fact]
        public void ShouldBeNotEither()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(3, 4),
                new TileInfo(5, 6)
            );

            Assert.False(subject.IsEither);
        }

        [Fact]
        public void ShouldBeNotEitherToo()
        {
            var subject = new TileSample(
                new TileInfo(1, 2),
                new TileInfo(1, 2),
                new TileInfo(1, 2)
            );

            Assert.False(subject.IsEither);
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
                    new TileInfo(-1, 2),
                    new TileInfo(3, 4),
                    new TileInfo(5, 6)
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
                new TileInfo(-3, 4),
                new TileInfo(5, 6)
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
                new TileInfo(-5, 6)
            );

            Assert.True(actual);
        }
    }
}
