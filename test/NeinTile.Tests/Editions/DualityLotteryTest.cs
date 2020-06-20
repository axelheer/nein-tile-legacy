using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Editions.Tests
{
    public class DualityLotteryTest
    {
        private readonly ITestOutputHelper output;

        public DualityLotteryTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShouldDrawRandomSample()
            => RunTests(100, new Tile(0, 100));

        private void RunTests(long maxValue, Tile expected)
        {
            const int testCount = 1000;

            var subject = new DualityLottery();

            var nope = 0;

            for (var i = 0; i < testCount; i++)
            {
                subject = (DualityLottery)subject.CreateNext();

                var actual = subject.Draw(maxValue);

                if (actual == null)
                {
                    nope += 1;
                    continue;
                }

                var (hint, tile) = actual.Value;

                output.WriteLine($"{hint} => {tile}");

                Assert.True(hint.IsSingle);

                Assert.Equal(expected, hint.First);
                Assert.Equal(expected, hint.Second);
                Assert.Equal(expected, hint.Third);

                Assert.Equal(expected, tile);
            }

            output.WriteLine($"Sorry: {nope}");

            Assert.NotEqual(0, nope);
            Assert.NotEqual(testCount, nope);
        }
    }
}
