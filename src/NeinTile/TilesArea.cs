using System;
using System.Collections.Generic;
using System.Linq;
using NeinTile.Abstractions;

namespace NeinTile
{
    [Serializable]
    public sealed class TilesArea
    {
        public IDealer Dealer { get; }
        public IMerger Merger { get; }

        public Tiles Tiles { get; }

        public TilesArea(Tiles tiles, IDealer dealer, IMerger merger)
        {
            Tiles = tiles ?? throw new ArgumentNullException(nameof(tiles));
            Dealer = dealer ?? throw new ArgumentNullException(nameof(dealer));
            Merger = merger ?? throw new ArgumentNullException(nameof(merger));
        }

        public bool CanMove(MoveDirection direction)
        {
            var enumerator = new MoveEnumerator(Tiles, direction);
            while (enumerator.MoveNext())
            {
                var move = enumerator.Current;
                var source = Tiles[move.Source];
                var target = Tiles[move.Target];
                if (Merger.CanMerge(source, target))
                    return true;
            }
            return false;
        }

        public TilesArea Move(MoveDirection direction, bool slippery, Tile nextTile)
        {
            var next = new Tiles(Tiles);
            var markers = new List<TileIndex>();
            var mergedEverything = false;
            var slipperyTurn = 0;

            do
            {
                slipperyTurn += 1;
                mergedEverything = true;
                var enumerator = new MoveEnumerator(next, direction);
                while (enumerator.MoveNext())
                {
                    var move = enumerator.Current;
                    if (next.IsMerge(move.Source))
                        continue;
                    if (next.IsMerge(move.Target))
                        continue;
                    var source = Tiles[move.Source];
                    var target = Tiles[move.Target];
                    if (Merger.CanMerge(source, target))
                    {
                        var merged = Merger.Merge(source, target);
                        next.Move(merged, move.Source, move.Target,
                            slipperyTurn
                        );
                        if (!markers.Contains(move.Marker))
                            markers.Add(move.Marker);
                        mergedEverything = false;
                    }
                }
            }
            while (slippery && !mergedEverything);

            if (slippery)
            {
                var freeIndices = next.Indices.Where(index
                    => next[index] == Tile.Empty).ToArray();
                foreach (var index in Dealer.Part(Dealer.Part(freeIndices)))
                    next[index] = nextTile;
            }
            else
            {
                foreach (var marker in Dealer.Part(markers.ToArray()))
                    next[marker] = nextTile;
            }

            return new TilesArea(next, Dealer.CreateNext(), Merger);
        }
    }
}
