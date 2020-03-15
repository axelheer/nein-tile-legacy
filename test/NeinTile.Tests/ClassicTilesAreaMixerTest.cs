using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class ClassicTilesAreaMixerTest
    {
        [Fact]
        public void ShouldShuffle()
        {
            var deck = new FakeTilesDeck()
            {
                OnShow = () => new TileInfo(1, 2)
            };
            deck.OnDraw = _ => deck;

            var subject = new ClassicTilesAreaMixer(deck, 3, 2, 1);

            var actual = subject.Shuffle();

            Assert.Equal(3, actual.GetLength(0));
            Assert.Equal(2, actual.GetLength(1));
            Assert.Equal(1, actual.GetLength(2));

            var drawn = 0;
            for (var colIndex = 0; colIndex < 3; colIndex++)
                for (var rowIndex = 0; rowIndex < 2; rowIndex++)
                    for (var layIndex = 0; layIndex < 1; layIndex++)
                        drawn = actual[colIndex, rowIndex, layIndex] != TileInfo.Empty ? drawn + 1 : drawn;

            Assert.Equal(3, drawn);
        }

        [Fact]
        public void ShouldCreateDeck()
        {
            var drawn = 0;
            var expected = new FakeTilesDeck()
            {
                OnShow = () => new TileInfo(1, 2)
            };
            expected.OnDraw = _ =>
            {
                drawn += 1;
                return expected;
            };

            var subject = new ClassicTilesAreaMixer(expected, 3, 2, 1);

            var actual = subject.CreateDeck();

            Assert.Equal(actual, expected);
            Assert.Equal(3, drawn);
        }
    }
}
