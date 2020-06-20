using System;
using NeinTile.Abstractions;
using NeinTile.Editions;

namespace NeinTile
{
    public enum GameEdition
    {
        Simple,
        Classic,
        Duality,
        Insanity
    }

    public static class GameEditionExtensions
    {
        public static IMaker Maker(this GameEdition edition)
        {
            return edition switch
            {
                GameEdition.Simple => new SimpleMaker(),
                GameEdition.Classic => new ClassicMaker(),
                GameEdition.Duality => new DualityMaker(),
                GameEdition.Insanity => new InsanityMaker(),
                _ => throw new ArgumentOutOfRangeException(nameof(edition))
            };
        }
    }
}
