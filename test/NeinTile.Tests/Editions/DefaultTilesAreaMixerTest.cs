using System;
using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Editions.Tests
{
    public class DefaultTilesAreaMixerTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var options = Assert.Throws<ArgumentNullException>(()
                => new DefaultTilesAreaMixer(null!));

            Assert.Equal(nameof(options), options.ParamName);
        }

        [Fact]
        public void ShouldShuffle()
        {
            var deck = new FakeTilesDeck()
            {
                OnShow = () => new TileInfo(1, 2)
            };
            deck.OnDraw = _ => deck;

            var subject = new DefaultTilesAreaMixer(new GameOptions(3, 2, 1));

            DoShuffle(deck, subject);

            var actual = subject.Tiles;

            Assert.NotNull(actual);

            Assert.Equal(3, actual!.GetLength(0));
            Assert.Equal(2, actual.GetLength(1));
            Assert.Equal(1, actual.GetLength(2));

            var drawn = 0;
            for (var colIndex = 0; colIndex < 3; colIndex++)
            {
                for (var rowIndex = 0; rowIndex < 2; rowIndex++)
                {
                    for (var layIndex = 0; layIndex < 1; layIndex++)
                        drawn = actual[colIndex, rowIndex, layIndex] != TileInfo.Empty ? drawn + 1 : drawn;
                }
            }

            Assert.Equal(3, drawn);
        }

        private static void DoShuffle(TilesDeck deck, DefaultTilesAreaMixer subject)
        {
            var tile = deck.Show();
            while (subject.AddNext(tile))
            {
                deck = deck.Draw();
                tile = deck.Show();
            }
        }
    }
}
