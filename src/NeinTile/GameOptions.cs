using System;

namespace NeinTile
{
    public class GameOptions
    {
        public int ColCount { get; }

        public int RowCount { get; }

        public int LayCount { get; }

        public GameOptions(int colCount, int rowCount, int layCount)
        {
            if (16 < colCount || colCount < 1)
                throw new ArgumentOutOfRangeException(nameof(colCount), colCount, null);
            if (16 < rowCount || rowCount < 1)
                throw new ArgumentOutOfRangeException(nameof(rowCount), rowCount, null);
            if (16 < layCount || layCount < 1)
                throw new ArgumentOutOfRangeException(nameof(layCount), layCount, null);

            ColCount = colCount;
            RowCount = rowCount;
            LayCount = layCount;
        }
    }
}
