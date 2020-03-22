using System;
using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Tests
{
    public class MoveEnumeratorTest
    {
        private readonly TileInfo[,,] tiles;
        private readonly ITestOutputHelper output;

        public MoveEnumeratorTest(ITestOutputHelper output)
        {
            tiles = new TileInfo[4, 4, 4];

            for (var colIndex = 0; colIndex < 4; colIndex++)
            {
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                {
                    for (var layIndex = 0; layIndex < 4; layIndex++)
                    {
                        tiles[colIndex, rowIndex, layIndex]
                            = new TileInfo(16 * layIndex + rowIndex * 4 + colIndex + 1, 0);
                    }
                }
            }

            this.output = output;
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
        public void ShouldMoveForward()
            => TestMove(MoveDirection.Forward, i => 48 - i, s => s + 16);

        [Fact]
        public void ShouldMoveBackward()
            => TestMove(MoveDirection.Backward, i => 17 + i, s => s - 16);

        private void TestMove(MoveDirection direction, Func<int, int> source, Func<int, int> target)
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, direction);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = source(iteration);
                var expectedTarget = target(expectedSource);

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

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
        public void ShouldMarkForward()
            => TestMark(MoveDirection.Forward, i => (3 - i % 4, 3 - i / 4 % 4, 0));

        [Fact]
        public void ShouldMarkBackward()
            => TestMark(MoveDirection.Backward, i => (i % 4, i / 4 % 4, 3));

        private void TestMark(MoveDirection direction, Func<int, (int colIndex, int rowIndex, int layIndex)> marking)
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, direction);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var (colIndex, rowIndex, layIndex) = marking(iteration);
                var expected = new MoveMarking(colIndex, rowIndex, layIndex);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration += 1;
            }

            Assert.Equal(48, iteration);
        }
    }
}
