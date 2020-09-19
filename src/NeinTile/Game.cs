using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeinTile
{
    [Serializable]
    public sealed class Game
    {
        public TilesDeck Deck { get; }

        public TilesArea Area { get; }

        public bool Slippery { get; }

        public Game? Previous { get; }

        public Game(TilesDeck deck, TilesArea area, bool slippery)
            : this(deck ?? throw new ArgumentNullException(nameof(deck)),
                   area ?? throw new ArgumentNullException(nameof(area)),
                   slippery,
                   default)
        {
        }

        private Game(TilesDeck deck, TilesArea area, bool slippery, Game? previous)
        {
            Deck = deck;
            Area = area;
            Slippery = slippery;
            Previous = previous;
        }

        public bool CanMove()
            => CanMove(MoveDirection.Right)
            || CanMove(MoveDirection.Left)
            || CanMove(MoveDirection.Up)
            || CanMove(MoveDirection.Down)
            || CanMove(MoveDirection.Front)
            || CanMove(MoveDirection.Back);

        public bool CanMove(MoveDirection direction)
            => Area.CanMove(direction);

        public Game Move(MoveDirection direction)
        {
            if (Area.CanMove(direction))
            {
                var nextArea = Area.Move(direction, Slippery, Deck.Tile);
                var nextDeck = Deck.Next(nextArea.Tiles.MaxValue);

                return new Game(nextDeck, nextArea, Slippery, this);
            }

            return this;
        }

#pragma warning disable MSLIB0003
#pragma warning disable SYSLIB0011

        public void Save(Stream inputStream)
        {
            if (inputStream is null)
                throw new ArgumentNullException(nameof(inputStream));

            var serializer = new BinaryFormatter();
            serializer.Serialize(inputStream, this);
        }

        public static Game Load(Stream outputStream)
        {
            if (outputStream is null)
                throw new ArgumentNullException(nameof(outputStream));

            var serializer = new BinaryFormatter();
            return (Game)serializer.Deserialize(outputStream);
        }
    }
}
