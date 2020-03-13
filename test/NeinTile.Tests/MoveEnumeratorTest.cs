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
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                    for (var lowIndex = 0; lowIndex < 4; lowIndex++)
                        tiles[colIndex, rowIndex, lowIndex]
                            = new TileInfo(16 * lowIndex + rowIndex * 4 + colIndex + 1, 0);

            this.output = output;
        }

        [Fact]
        public void ShouldMoveRight()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Right);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = 63 - iteration - iteration / 3;
                var expectedTarget = expectedSource + 1;

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMoveLeft()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Left);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = 2 + iteration + iteration / 3;
                var expectedTarget = expectedSource - 1;

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMoveUp()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Up);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = 60 - iteration - 4 * (iteration / 12);
                var expectedTarget = expectedSource + 4;

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMoveDown()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Down);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = 5 + iteration + 4 * (iteration / 12);
                var expectedTarget = expectedSource - 4;

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMoveForward()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Forward);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = 48 - iteration;
                var expectedTarget = expectedSource + 16;

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMoveBackward()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Backward);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expectedSource = 17 + iteration;
                var expectedTarget = expectedSource - 16;

                var (actualSource, actualTarget) = subject.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkRight()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Right);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expected = new MoveMarking(0, 3 - iteration / 3 % 4, 3 - iteration / 12);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration +=1;
            }

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkLeft()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Left);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expected = new MoveMarking(3, iteration / 3 % 4, iteration / 12);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration +=1;
            }

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkUp()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Up);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expected = new MoveMarking(3 - iteration % 4, 0, 3 - iteration / 12);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration +=1;
            }

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkDown()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Down);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expected = new MoveMarking(iteration % 4, 3, iteration / 12);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration +=1;
            }

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkForward()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Forward);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expected = new MoveMarking(3 - iteration % 4, 3 - iteration / 4 % 4, 0);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration +=1;
            }

            Assert.Equal(48, iteration);
        }

        [Fact]
        public void ShouldMarkBackward()
        {
            var iteration = 0;

            var subject = new MoveEnumerator(tiles, MoveDirection.Backward);
            while (subject.MoveNext())
            {
                output.WriteLine(subject.Current.ToString());

                var expected = new MoveMarking(iteration % 4, iteration / 4 % 4, 3);

                var (source, target) = subject.Current;
                var actual = subject.Update(source, target);

                Assert.Equal(expected, actual);

                iteration +=1;
            }

            Assert.Equal(48, iteration);
        }
    }
}
