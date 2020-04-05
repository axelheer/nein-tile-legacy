using System;
using System.Collections.Generic;
using NeinTile.Abstractions;
using NeinTile.Editions;

namespace NeinTile
{
    public abstract class GameFactory
    {
        private static readonly IDictionary<string, GameFactory> Factories
            = new Dictionary<string, GameFactory>(OptionComparer.Instance)
            {
                ["simple"] = new SimpleGameFactory(),
                ["classic"] = new ClassicGameFactory(),
                ["duality"] = new DualityGameFactory(),
                ["insanity"] = new InsanityGameFactory()
            };

        public static GameState CreateNew(string name, GameOptions options)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            if (!Factories.TryGetValue(name, out var factory))
                throw new ArgumentOutOfRangeException(nameof(name), name, null);

            return factory.CreateNew(options);
        }

        protected virtual ITilesAreaLottery CreateAreaLottery(GameOptions options)
            => new DefaultTilesAreaLottery();

        protected abstract ITilesAreaMerger CreateAreaMerger(GameOptions options);

        protected virtual ITilesAreaMixer CreateAreaMixer(GameOptions options)
            => new DefaultTilesAreaMixer(options);

        protected abstract ITilesDeckLottery CreateDeckLottery(GameOptions options);

        protected abstract ITilesDeckMixer CreateDeckMixer(GameOptions options);

        protected virtual GameState CreateNew(GameOptions options)
        {
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
