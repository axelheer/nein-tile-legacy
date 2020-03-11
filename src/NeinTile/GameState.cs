using System;

namespace NeinTile
{
    public class GameState
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

        public virtual GameState Move(MoveDirection direction)
        {
            if (!Area.CanMove(direction))
                throw new InvalidOperationException($"Unable to move into direction '{direction}'.");

            var nextDeck = Deck.Draw(out var nextTile);
            var nextArea = Area.Move(direction, nextTile);

            return new GameState(nextDeck, nextArea, this);
        }
    }
}
