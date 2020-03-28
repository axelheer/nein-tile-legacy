using System;
using System.Collections.Generic;

namespace NeinTile
{
    internal sealed class OptionComparer : IEqualityComparer<string>
    {
        public static IEqualityComparer<string> Instance = new OptionComparer();

        public bool Equals(string? x, string? y)
            => string.Equals(x, y, StringComparison.OrdinalIgnoreCase)
            || char.ToLowerInvariant(x?[0] ?? default) == char.ToLowerInvariant(y?[0] ?? default);

        public int GetHashCode(string obj)
            => char.ToLowerInvariant(obj[0]);
    }
}
