using System;

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
            => Area.CanMove(MoveDirection.Right)
            || Area.CanMove(MoveDirection.Left)
            || Area.CanMove(MoveDirection.Up)
            || Area.CanMove(MoveDirection.Down)
            || Area.CanMove(MoveDirection.Forward)
            || Area.CanMove(MoveDirection.Backward);

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
    }
}
