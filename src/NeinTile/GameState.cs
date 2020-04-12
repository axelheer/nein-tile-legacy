using System;
using System.IO;
using System.Threading.Tasks;

namespace NeinTile
{
    public sealed class GameState
    {
        public TilesDeck Deck { get; }

        public TilesArea Area { get; }

        public GameState? Previous { get; }

        public GameState(TilesDeck deck, TilesArea area)
        {
            Deck = deck ?? throw new ArgumentNullException(nameof(deck));
            Area = area ?? throw new ArgumentNullException(nameof(area));
        }

        private GameState(TilesDeck deck, TilesArea area, GameState previous)
            : this(deck, area)
        {
            Previous = previous ?? throw new ArgumentNullException(nameof(previous));
        }

        public bool CanMove()
            => CanMove(MoveDirection.Right)
            || CanMove(MoveDirection.Left)
            || CanMove(MoveDirection.Up)
            || CanMove(MoveDirection.Down)
            || CanMove(MoveDirection.Forward)
            || CanMove(MoveDirection.Backward);

        public bool CanMove(MoveDirection direction)
            => Area.CanMove(direction);

        public GameState Move(MoveDirection direction)
        {
            if (Area.CanMove(direction))
            {
                var nextTile = Deck.Show();
                var nextArea = Area.Move(direction, nextTile);
                var nextDeck = Deck.Draw(nextArea);

                return new GameState(nextDeck, nextArea, this);
            }

            return this;
        }

#pragma warning disable CA1801
#pragma warning disable CA1822

        public Task SaveAsync(Stream stream)
            => throw new NotImplementedException();

        public static Task<GameState> LoadAsync(Stream stream)
            => throw new NotImplementedException();
    }
}
