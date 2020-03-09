using System;

namespace NeinTile
{
    public static class GameStateExtensions
    {
        public static TileInfo[] Preview(this GameState state)
        {
            if (state is null)
                throw new ArgumentNullException(nameof(state));

            return state.Deck.Preview();
        }

        public static bool CanMove(this GameState state, MoveDirection direction)
        {
            if (state is null)
                throw new ArgumentNullException(nameof(state));

            return state.Area.CanMove(direction);
        }
    }
}
