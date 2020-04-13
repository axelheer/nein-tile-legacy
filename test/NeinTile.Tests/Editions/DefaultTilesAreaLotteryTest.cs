using System;
using System.Collections.Generic;
using Xunit;

namespace NeinTile.Editions.Tests
{
    public class DefaultTilesAreaLotteryTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var subject = new DefaultTilesAreaLottery();

            var markings = Assert.Throws<ArgumentNullException>(()
                => subject.Draw(null!));

            Assert.Equal(nameof(markings), markings.ParamName);
        }

        [Fact]
        public void ShouldDrawSameMove()
        {
            var expected = new MoveMarking(1, 2, 3);
            var unexpected = new MoveMarking(6, 6, 6);

            for (var i = 0; i < 100; i++)
            {
                var subject = new DefaultTilesAreaLottery();

                _ = subject.Draw(new[] { expected });
                subject = (DefaultTilesAreaLottery)subject.CreateNext();

                var actualItems = subject.Draw(new[] { unexpected, expected, unexpected });

                var actual = Assert.Single(actualItems);

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
                var subject = new DefaultTilesAreaLottery();

                _ = subject.Draw(new[] { unexpected });
                subject = (DefaultTilesAreaLottery)subject.CreateNext();

                var actualItems = subject.Draw(expected);

                var actual = Assert.Single(actualItems);

                Assert.Contains(actual, expected);

                _ = results.Add(actual);
            }

            Assert.NotEqual(1, results.Count);
        }
    }
}
