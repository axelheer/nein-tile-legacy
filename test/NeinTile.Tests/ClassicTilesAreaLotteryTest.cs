using System.Collections.Generic;
using Xunit;

namespace NeinTile.Tests
{
    public class ClassicTilesAreaLotteryTest
    {
        [Fact]
        public void ShouldDrawSameMove()
        {
            var expected = new MoveMarking(1, 2, 3);
            var unexpected = new MoveMarking(6, 6, 6);

            for (var i = 0; i < 100; i++)
            {
                ITilesAreaLottery subject = new ClassicTilesAreaLottery();

                subject.Draw(new[] { expected });
                subject = subject.CreateNext();

                var actual = subject.Draw(new[] { unexpected, expected, unexpected });

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ShouldDrawRandomMove()
        {
            var expected = new[]
            {
                new MoveMarking(1, 2, 3),
                new MoveMarking(2, 3, 4),
                new MoveMarking(3, 4, 5)
            };
            var unexpected = new MoveMarking(6, 6, 6);

            var results = new HashSet<MoveMarking>();
            for (var i = 0; i < 100; i++)
            {
                ITilesAreaLottery subject = new ClassicTilesAreaLottery();

                subject.Draw(new[] { unexpected });
                subject = subject.CreateNext();

                var actual = subject.Draw(expected);

                Assert.Contains(actual, expected);

                results.Add(actual);
            }

            Assert.NotEqual(1, results.Count);
        }
    }
}
