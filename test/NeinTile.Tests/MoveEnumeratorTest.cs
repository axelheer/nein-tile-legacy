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

            for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                for (var colIndex = 0; colIndex < 4; colIndex++)
                    for (var lowIndex = 0; lowIndex < 4; lowIndex++)
                        tiles[rowIndex, colIndex, lowIndex]
                            = new TileInfo(16 * lowIndex + colIndex * 4 + rowIndex + 1, 0);

            this.output = output;
        }

        [Fact]
        public void ShouldMoveRight()
        {
            var iteration = 0;

            var enumerator = new MoveEnumerator(tiles, MoveDirection.Right);
            while (enumerator.MoveNext())
            {
                output.WriteLine(enumerator.Current.ToString());

                var expectedSource = 1 + iteration + iteration / 3;
                var expectedTarget = expectedSource + 1;

                var (actualSource, actualTarget) = enumerator.Current;

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

            var enumerator = new MoveEnumerator(tiles, MoveDirection.Left);
            while (enumerator.MoveNext())
            {
                output.WriteLine(enumerator.Current.ToString());

                var expectedSource = 64 - iteration - iteration / 3;
                var expectedTarget = expectedSource - 1;

                var (actualSource, actualTarget) = enumerator.Current;

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

            var enumerator = new MoveEnumerator(tiles, MoveDirection.Up);
            while (enumerator.MoveNext())
            {
                output.WriteLine(enumerator.Current.ToString());

                var expectedSource = 1 + iteration + 4 * (iteration / 12);
                var expectedTarget = expectedSource + 4;

                var (actualSource, actualTarget) = enumerator.Current;

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

            var enumerator = new MoveEnumerator(tiles, MoveDirection.Down);
            while (enumerator.MoveNext())
            {
                output.WriteLine(enumerator.Current.ToString());

                var expectedSource = 64 - iteration - 4 * (iteration / 12);
                var expectedTarget = expectedSource - 4;

                var (actualSource, actualTarget) = enumerator.Current;

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

            var enumerator = new MoveEnumerator(tiles, MoveDirection.Forward);
            while (enumerator.MoveNext())
            {
                output.WriteLine(enumerator.Current.ToString());

                var expectedSource = 1 + iteration;
                var expectedTarget = expectedSource + 16;

                var (actualSource, actualTarget) = enumerator.Current;

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

            var enumerator = new MoveEnumerator(tiles, MoveDirection.Backward);
            while (enumerator.MoveNext())
            {
                output.WriteLine(enumerator.Current.ToString());

                var expectedSource = 64 - iteration;
                var expectedTarget = expectedSource - 16;

                var (actualSource, actualTarget) = enumerator.Current;

                Assert.Equal(expectedSource, actualSource.Value);
                Assert.Equal(expectedTarget, actualTarget.Value);

                iteration +=1;
            };

            Assert.Equal(48, iteration);
        }
    }
}
