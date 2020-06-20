using System;
using System.Linq;

namespace NeinTile
{
    [Serializable]
    public sealed class Tiles
    {
        public int Count { get; }

        public int ColCount { get; }
        public int RowCount { get; }
        public int LayCount { get; }

        private readonly Tile[] tiles;
        private readonly bool[] merge;
        private readonly int[] moves;

        public Tiles(int colCount, int rowCount, int layCount)
        {
            if (colCount < 0)
                throw new ArgumentOutOfRangeException(nameof(colCount));
            if (rowCount < 0)
                throw new ArgumentOutOfRangeException(nameof(rowCount));
            if (layCount < 0)
                throw new ArgumentOutOfRangeException(nameof(layCount));

            Count = colCount * rowCount * layCount;

            ColCount = colCount;
            RowCount = rowCount;
            LayCount = layCount;

            tiles = new Tile[Count];
            merge = new bool[Count];
            moves = new int[Count];
        }

        public Tiles(Tiles tiles)
        {
            if (tiles is null)
                throw new ArgumentNullException(nameof(tiles));

            Count = tiles.Count;

            ColCount = tiles.ColCount;
            RowCount = tiles.RowCount;
            LayCount = tiles.LayCount;

            this.tiles = tiles.tiles;
            merge = new bool[Count];
            moves = new int[Count];
        }

        public long MinValue
            => tiles.Min(t => t.Value);

        public long MaxValue
            => tiles.Max(t => t.Value);

        public long TotalScore
            => tiles.Sum(t => t.Score);

        public TileIndex[] Indices
        {
            get
            {
                var cols = Enumerable.Range(0, ColCount);
                var rows = Enumerable.Range(0, RowCount);
                var lays = Enumerable.Range(0, LayCount);

                return (
                    from col in cols
                    from row in rows
                    from lay in lays
                    select new TileIndex(col, row, lay)
                ).ToArray();
            }
        }

        public bool IsMerge(int col, int row, int lay)
            => IsMerge(new TileIndex(col, row, lay));

        public bool IsMerge(TileIndex index)
            => merge[ActualIndex(index)];

        public int GetMoves(int col, int row, int lay)
            => GetMoves(new TileIndex(col, row, lay));

        public int GetMoves(TileIndex index)
            => moves[ActualIndex(index)];

        public void Move(Tile tile, TileIndex source, TileIndex target, int turn)
        {
            var sourceIndex = ActualIndex(source);
            var targetIndex = ActualIndex(target);

            if (tiles[targetIndex] != Tile.Empty)
                merge[targetIndex] = true;

            tiles[sourceIndex] = Tile.Empty;
            tiles[targetIndex] = tile;


            moves[targetIndex + (sourceIndex - targetIndex) * turn] += 1;
        }

        public Tile this[int col, int row, int lay]
        {
            get => this[new TileIndex(col, row, lay)];
            set => this[new TileIndex(col, row, lay)] = value;
        }

        public Tile this[TileIndex index]
        {
            get => tiles[ActualIndex(index)];
            set => tiles[ActualIndex(index)] = value;
        }

        private int ActualIndex(TileIndex index)
        {
            return !IsIndexValid(index)
                ? throw new ArgumentOutOfRangeException(nameof(index))
                : index.Col + index.Row * ColCount + index.Lay * ColCount * RowCount;
        }

        private bool IsIndexValid(TileIndex index)
        {
            return 0 <= index.Col && index.Col < ColCount
                && 0 <= index.Row && index.Row < RowCount
                && 0 <= index.Lay && index.Lay < LayCount;
        }
    }
}
