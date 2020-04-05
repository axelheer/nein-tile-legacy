using System;
using Xunit;

namespace NeinTile.Tests
{
    public class GameOptionsTest
    {
        [Fact]
        public void ShouldHandleInvalid()
        {
            var colCount = Assert.Throws<ArgumentOutOfRangeException>(()
                => new GameOptions(0, 1, 1));

            Assert.Equal(nameof(colCount), colCount.ParamName);
            Assert.Equal(0, colCount.ActualValue);

            var rowCount = Assert.Throws<ArgumentOutOfRangeException>(()
                => new GameOptions(1, 0, 1));

            Assert.Equal(nameof(rowCount), rowCount.ParamName);
            Assert.Equal(0, rowCount.ActualValue);

            var layCount = Assert.Throws<ArgumentOutOfRangeException>(()
                => new GameOptions(1, 1, 0));

            Assert.Equal(nameof(layCount), layCount.ParamName);
            Assert.Equal(0, layCount.ActualValue);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var subject = new GameOptions(1, 2, 3);

            Assert.Equal(1, subject.ColCount);
            Assert.Equal(2, subject.RowCount);
            Assert.Equal(3, subject.LayCount);
        }
    }
}
