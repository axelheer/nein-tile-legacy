using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeinTile
{
    [Serializable]
    public sealed class GameState
    {
        public TilesDeck Deck { get; }

        public TilesArea Area { get; }

        public GameState? Previous { get; }

        public GameState(TilesDeck deck, TilesArea area)
            : this(deck ?? throw new ArgumentNullException(nameof(deck)),
                   area ?? throw new ArgumentNullException(nameof(area)),
                   default)
        {
        }

        private GameState(TilesDeck deck, TilesArea area, GameState? previous)
        {
            Deck = deck;
            Area = area;
            Previous = previous;
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

        public void Save(Stream inputStream)
        {
            if (inputStream is null)
                throw new ArgumentNullException(nameof(inputStream));

            var serializer = new BinaryFormatter();
            serializer.Serialize(inputStream, this);
        }

        public static GameState Load(Stream outputStream)
        {
            if (outputStream is null)
                throw new ArgumentNullException(nameof(outputStream));

            var serializer = new BinaryFormatter();
            return (GameState)serializer.Deserialize(outputStream);
        }
    }
}
