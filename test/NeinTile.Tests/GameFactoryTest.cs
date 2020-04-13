using System;
using Xunit;

namespace NeinTile.Tests
{
    public class GameFactoryTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var options = Assert.Throws<ArgumentNullException>(()
                => GameFactory.CreateNew(GameEdition.Classic, null!));

            Assert.Equal(nameof(options), options.ParamName);
        }

        [Fact]
        public void ShouldHandleInvalid()
        {
            var invalid = (GameEdition)int.MinValue;

            var edition = Assert.Throws<ArgumentOutOfRangeException>(()
                => GameFactory.CreateNew(invalid, new GameOptions(4, 4, 1)));

            Assert.Equal(nameof(edition), edition.ParamName);
            Assert.Equal(invalid, edition.ActualValue);
        }

        [Theory]
        [InlineData(GameEdition.Simple)]
        [InlineData(GameEdition.Classic)]
        [InlineData(GameEdition.Duality)]
        [InlineData(GameEdition.Insanity)]
        public void ShouldCreateByEdition(GameEdition edition)
        {
            var actual = GameFactory.CreateNew(edition, new GameOptions(4, 4, 1));

            Assert.NotNull(actual);
        }
    }
}
