using System;
using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Tests
{
    public class MoveEnumeratorTest
    {
        private readonly Tiles tiles;
        private readonly ITestOutputHelper output;

        public MoveEnumeratorTest(ITestOutputHelper output)
        {
            tiles = new Tiles(4, 4, 4);

            for (var colIndex = 0; colIndex < 4; colIndex++)
            {
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                {
                    for (var layIndex = 0; layIndex < 4; layIndex++)
                    {
                        tiles[colIndex, rowIndex, layIndex]
                            = new Tile(16 * layIndex + rowIndex * 4 + colIndex + 1, 0);
                    }
                }
            }

            this.output = output;
        }

        [Fact]
        public void ShouldHandleNull()
        {
            var tiles = Assert.Throws<ArgumentNullException>(()
                => new MoveEnumerator(null!, default));

            Assert.Equal(nameof(tiles), tiles.ParamName);
        }

        [Theory]
        [InlineData(1, 2, 2, MoveDirection.Right)]
        [InlineData(2, 1, 2, MoveDirection.Up)]
        [InlineData(2, 2, 1, MoveDirection.Front)]
        public void ShouldFreeze(int colCount, int rowCount, int layCount, MoveDirection direction)
        {
            var tiles = new Tiles(colCount, rowCount, layCount);

            var subject = new MoveEnumerator(tiles, direction);

            Assert.False(subject.MoveNext());
        }

        [Fact]
        public void ShouldMoveRight()
            => TestMove(MoveDirection.Right, i => 63 - i - i / 3, s => s + 1);

        [Fact]
        public void ShouldMoveLeft()
            => TestMove(MoveDirection.Left, i => 2 + i + i / 3, s => s - 1);

        [Fact]
        public void ShouldMoveUp()
            => TestMove(MoveDirection.Up, i => 60 - i - 4 * (i / 12), s => s + 4);

        [Fact]
        public void ShouldMoveDown()
            => TestMove(MoveDirection.Down, i => 5 + i + 4 * (i / 12), s => s - 4);

        [Fact]
        public void ShouldMoveFront()
            => TestMove(MoveDirection.Front, i => 48 - i, s => s + 16);

        [Fact]
        public void ShouldMoveBack()
            => TestMove(MoveDirection.Back, i => 17 + i, s => s - 16);

        private void TestMove(MoveDirection direction, Func<int, int> source, Func<int, int> target)
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, direction);
            while (subject.MoveNext())
            {
                output.WriteLine($"{subject.Current}");

                var expectedSource = source(iteration);
                var expectedTarget = target(expectedSource);

                var (actualSource, actualTarget, _) = subject.Current;

                Assert.Equal(expectedSource, tiles[actualSource].Value);
                Assert.Equal(expectedTarget, tiles[actualTarget].Value);

                iteration += 1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkRight()
            => TestMark(MoveDirection.Right, i => (0, 3 - i / 3 % 4, 3 - i / 12));

        [Fact]
        public void ShouldMarkLeft()
            => TestMark(MoveDirection.Left, i => (3, i / 3 % 4, i / 12));

        [Fact]
        public void ShouldMarkUp()
            => TestMark(MoveDirection.Up, i => (3 - i % 4, 0, 3 - i / 12));

        [Fact]
        public void ShouldMarkDown()
            => TestMark(MoveDirection.Down, i => (i % 4, 3, i / 12));

        [Fact]
        public void ShouldMarkFront()
            => TestMark(MoveDirection.Front, i => (3 - i % 4, 3 - i / 4 % 4, 0));

        [Fact]
        public void ShouldMarkBack()
            => TestMark(MoveDirection.Back, i => (i % 4, i / 4 % 4, 3));

        private void TestMark(MoveDirection direction, Func<int, (int colIndex, int rowIndex, int layIndex)> marking)
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, direction);
            while (subject.MoveNext())
            {
                var (colIndex, rowIndex, layIndex) = marking(iteration);
                var expected = new TileIndex(colIndex, rowIndex, layIndex);

                output.WriteLine($"{subject.Current} => {expected}");

                var (_, _, actual) = subject.Current;

                Assert.Equal(expected, actual);

                iteration += 1;
            }

            Assert.Equal(48, iteration);
        }
    }
}
