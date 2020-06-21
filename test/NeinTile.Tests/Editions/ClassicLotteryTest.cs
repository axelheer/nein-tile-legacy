using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Editions.Tests
{
    public class ClassicLotteryTest
    {
        private readonly ITestOutputHelper output;

        public ClassicLotteryTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShouldDrawEmptySample()
            => RunTests(4);

        [Fact]
        public void ShouldDrawSingleSample()
            => RunTests(48, new Tile(6, 9));

        [Fact]
        public void ShouldDrawEitherSample()
            => RunTests(96, new Tile(6, 9), new Tile(12, 27));

        [Fact]
        public void ShouldDrawRandomSample()
        {
            RunTests(768,
                new Tile(6, 9),
                new Tile(12, 27),
                new Tile(24, 81),
                new Tile(48, 243),
                new Tile(96, 729)
            );
        }

        private void RunTests(long maxValue, params Tile[] expected)
        {
            const int testCount = 1000;

            var subject = new ClassicLottery();

            var nope = 0;

            for (var i = 0; i < testCount; i++)
            {
                subject = (ClassicLottery)subject.CreateNext();

                var actual = subject.Draw(maxValue);

                if (actual == null)
                {
                    nope += 1;
                    continue;
                }

                var (hint, tile) = actual.Value;

                output.WriteLine($"{hint} => {tile}");

                Assert.Contains(hint.First, expected);
                Assert.Contains(hint.Second, expected);
                Assert.Contains(hint.Third, expected);

                Assert.Contains(tile, new[] { hint.First, hint.Second, hint.Third });

                if (!hint.IsSingle)
                {
                    Assert.Equal(hint.Second.Value, hint.First.Value * 2);
                    Assert.Equal(hint.Second.Score, hint.First.Score * 3);

                    if (!hint.IsEither)
                    {
                        Assert.Equal(hint.Third.Value, hint.Second.Value * 2);
                        Assert.Equal(hint.Third.Score, hint.Second.Score * 3);
                    }
                }
            }

            output.WriteLine($"Sorry: {nope}");

            if (expected.Length == 0)
            {
                Assert.Equal(testCount, nope);
            }
            else
            {
                Assert.NotEqual(0, nope);
                Assert.NotEqual(testCount, nope);
            }
        }
    }
}
