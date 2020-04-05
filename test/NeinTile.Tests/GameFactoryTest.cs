using System;
using Xunit;

namespace NeinTile.Tests
{
    public class GameFactoryTest
    {
        [Fact]
        public void ShouldHandleNull()
        {
            var name = Assert.Throws<ArgumentNullException>(()
                => GameFactory.CreateNew(null!, new GameOptions(4, 4, 1)));

            Assert.Equal(nameof(name), name.ParamName);

            var options = Assert.Throws<ArgumentNullException>(()
                => GameFactory.CreateNew("name", null!));

            Assert.Equal(nameof(options), options.ParamName);
        }

        [Fact]
        public void ShouldHandleInvalid()
        {
            var name = Assert.Throws<ArgumentOutOfRangeException>(()
                => GameFactory.CreateNew("narf", new GameOptions(4, 4, 1)));

            Assert.Equal(nameof(name), name.ParamName);
            Assert.Equal("narf", name.ActualValue);
        }

        [Theory]
        [InlineData("s")]
        [InlineData("c")]
        [InlineData("d")]
        [InlineData("i")]
        public void ShouldCreateByKey(string key)
        {
            var actual = GameFactory.CreateNew(key, new GameOptions(4, 4, 1));

            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("simple")]
        [InlineData("classic")]
        [InlineData("duality")]
        [InlineData("insanity")]
        public void ShouldCreateByName(string name)
        {
            var actual = GameFactory.CreateNew(name, new GameOptions(4, 4, 1));

            Assert.NotNull(actual);
        }
    }
}
