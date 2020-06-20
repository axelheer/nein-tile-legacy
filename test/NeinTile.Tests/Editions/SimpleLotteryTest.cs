using NeinTile.Fakes;
using Xunit;
using Xunit.Abstractions;

namespace NeinTile.Editions.Tests
{
    public class SimpleLotteryTest
    {
        private readonly ITestOutputHelper output;

        public SimpleLotteryTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShouldDrawRandomSample()
            => RunTests(new Tile(4, 6));

        private void RunTests(Tile expected)
        {
            const int testCount = 1000;

            var subject = new SimpleLottery();

            var nope = 0;

            for (var i = 0; i < testCount; i++)
            {
                subject = (SimpleLottery)subject.CreateNext();

                var actual = subject.Draw(0);

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
