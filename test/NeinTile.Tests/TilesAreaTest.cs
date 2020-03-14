using NeinTile.Fakes;
using Xunit;

namespace NeinTile.Tests
{
    public class TilesAreaTest
    {
        [Fact]
        public void ShouldCanMove()
        {
            var subject = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(1, 0) }, { new TileInfo(3, 0) } },
                        { { new TileInfo(1, 0) }, { new TileInfo(4, 0) } }
                    }
                },
                new FakeTilesAreaMerger()
                {
                    OnCanMerge = (source, target) => source == target
                },
                new FakeTilesAreaLottery()
            );

            var actual = subject.CanMove(MoveDirection.Right);

            Assert.True(actual);
            Assert.Equal(2, subject.ColCount);
            Assert.Equal(2, subject.RowCount);
            Assert.Equal(1, subject.LowCount);
            Assert.Equal(1, subject[0, 0, 0].Value);
            Assert.Equal(1, subject[1, 0, 0].Value);
            Assert.Equal(3, subject[0, 1, 0].Value);
            Assert.Equal(4, subject[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldNotCanMove()
        {
            var subject = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(1, 0) }, { new TileInfo(3, 0) } },
                        { { new TileInfo(2, 0) }, { new TileInfo(4, 0) } }
                    }
                },
                new FakeTilesAreaMerger()
                {
                    OnCanMerge = (source, target) => source == target
                },
                new FakeTilesAreaLottery()
            );

            var actual = subject.CanMove(MoveDirection.Up);

            Assert.False(actual);
            Assert.Equal(2, subject.ColCount);
            Assert.Equal(2, subject.RowCount);
            Assert.Equal(1, subject.LowCount);
            Assert.Equal(1, subject[0, 0, 0].Value);
            Assert.Equal(2, subject[1, 0, 0].Value);
            Assert.Equal(3, subject[0, 1, 0].Value);
            Assert.Equal(4, subject[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldMove()
        {
            var subject = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(1, 0) }, { new TileInfo(3, 0) } },
                        { { new TileInfo(1, 0) }, { new TileInfo(4, 0) } }
                    }
                },
                new FakeTilesAreaMerger()
                {
                    OnCanMerge = (source, target) => source == target,
                    OnMerge = (source, target) =>
                    {
                        Assert.Equal(new TileInfo(1, 0), source);
                        Assert.Equal(new TileInfo(1, 0), target);

                        return (new TileInfo(2, 0), new TileInfo(0, 0));
                    }
                },
                new FakeTilesAreaLottery()
                {
                    OnDraw = markings => markings[0]
                }
            );

            var actual = subject.Move(MoveDirection.Right, new TileInfo(1, 0));

            Assert.Equal(1, actual[0, 0, 0].Value);
            Assert.Equal(2, actual[1, 0, 0].Value);
            Assert.Equal(3, actual[0, 1, 0].Value);
            Assert.Equal(4, actual[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldMoveCopy()
        {
            var subject = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                    {
                        { { new TileInfo(1, 0) }, { new TileInfo(3, 0) } },
                        { { new TileInfo(1, 0) }, { new TileInfo(4, 0) } }
                    }
                },
                new FakeTilesAreaMerger(),
                new FakeTilesAreaLottery()
            );

            subject.Move(MoveDirection.Right, TileInfo.Empty);

            Assert.Equal(1, subject[0, 0, 0].Value);
            Assert.Equal(1, subject[1, 0, 0].Value);
            Assert.Equal(3, subject[0, 1, 0].Value);
            Assert.Equal(4, subject[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldMoveSameMerger()
        {
            var subject = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                },
                new FakeTilesAreaMerger()
                {
                    OnCanMerge = (_, __) => true,
                    OnMerge = (_, __) => (new TileInfo(11, 0), new TileInfo(7, 0))
                },
                new FakeTilesAreaLottery()
            );

            var actual = subject.Move(MoveDirection.Right, TileInfo.Empty)
                                .Move(MoveDirection.Right, new TileInfo(13, 0));

            Assert.Equal(13, actual[0, 0, 0].Value);
            Assert.Equal(11, actual[1, 0, 0].Value);
            Assert.Equal(7, actual[0, 1, 0].Value);
            Assert.Equal(11, actual[1, 1, 0].Value);
        }

        [Fact]
        public void ShouldMoveNewLottery()
        {
            var subject = new TilesArea(
                new FakeTilesAreaMixer()
                {
                    OnShuffle = () => new TileInfo[2, 2, 1]
                },
                new FakeTilesAreaMerger()
                {
                    OnCanMerge = (_, __) => true,
                    OnMerge = (_, __) => (new TileInfo(11, 0), new TileInfo(7, 0))
                },
                new FakeTilesAreaLottery()
                {
                    OnCreateNext = () => new FakeTilesAreaLottery()
                    {
                        OnDraw = _ => new MoveMarking(0, 1, 0)
                    }
                }
            );

            var actual = subject.Move(MoveDirection.Right, TileInfo.Empty)
                                .Move(MoveDirection.Right, new TileInfo(13, 0));

            Assert.Equal(7, actual[0, 0, 0].Value);
            Assert.Equal(11, actual[1, 0, 0].Value);
            Assert.Equal(13, actual[0, 1, 0].Value);
            Assert.Equal(11, actual[1, 1, 0].Value);
        }
    }
}
