using System;
using Xunit;

namespace NeinTile.Tests
{
    public class MoveMarkingTest
    {
        [Fact]
        public void ShouldBeEmpty()
        {
            var subject = new MoveMarking();

            Assert.Equal(MoveMarking.Empty, subject);
        }

        [Fact]
        public void ShouldAssignValues()
        {
            var (colIndex, rowIndex, lowIndex) = new MoveMarking(1, 2, 3);

            Assert.Equal(1, colIndex);
            Assert.Equal(2, rowIndex);
            Assert.Equal(3, lowIndex);
        }

        [Fact]
        public void ShouldCombineHashCode()
        {
            var subject = new MoveMarking(7, 11, 13);

            var expected = HashCode.Combine(subject.ColIndex, subject.RowIndex, subject.LowIndex);

            var actual = subject.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject.Equals(new MoveMarking(11, 38, 81));

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqual()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject.Equals(new MoveMarking(-1, 38, 81));

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualAsObject()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject.Equals((object)subject);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualAsObject()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject.Equals(new object());

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingEquals()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject == new MoveMarking(11, 38, 81);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingEquals()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject == new MoveMarking(11, -1, 81);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeEqualUsingNotEquals()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject != new MoveMarking(11, 38, 81);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldBeNotEqualUsingNotEquals()
        {
            var subject = new MoveMarking(11, 38, 81);

            var actual = subject != new MoveMarking(11, 38, -1);

            Assert.True(actual);
        }
    }
}
