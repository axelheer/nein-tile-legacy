using System;
using System.Collections.Generic;

namespace NeinTile
{
    public abstract class GameFactory
    {
        private static readonly IDictionary<string, GameFactory> factories
            = new Dictionary<string, GameFactory>(StringComparer.OrdinalIgnoreCase)
        {
            ["classic"] = new ClassicGameFactory()
        };

        private static void Register(string name, GameFactory factory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("message", nameof(name));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            factories[name] = factory;
        }

        public static GameState CreateNew(string name, GameOptions options)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("message", nameof(name));
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            if (factories.TryGetValue(name, out var factory))
                return factory.CreateNew(options);

            throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }

        public abstract ITilesDeckMixer CreateDeckMixer(GameOptions options);
        public abstract ITilesDeckLottery CreateDeckLottery(GameOptions options);
        public abstract ITilesAreaMixer CreateAreaMixer(GameOptions options);
        public abstract ITilesAreaMerger CreateAreaMerger(GameOptions options);
        public abstract ITilesAreaLottery CreateAreaLottery(GameOptions options);

        public virtual GameState CreateNew(GameOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            var deckMixer = CreateDeckMixer(options);
            var deckLottery = CreateDeckLottery(options);

            var deck = new TilesDeck(deckMixer, deckLottery);

            var areaMixer = CreateAreaMixer(options);
            var areaMerger = CreateAreaMerger(options);
            var areaLottery = CreateAreaLottery(options);

            var tile = deck.Show();
            while (areaMixer.AddNext(tile))
            {
                deck = deck.Draw();
                tile = deck.Show();
            }

            var area = new TilesArea(areaMixer, areaMerger, areaLottery);

            return new GameState(deck, area);
        }
    }
}
