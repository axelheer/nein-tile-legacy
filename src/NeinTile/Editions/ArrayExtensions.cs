using System;

internal static class ArrayExtensions
{
    public static T[] Shuffle<T>(this T[] items, Random random)
    {
        for (var index = 0; index < items.Length - 1; index++)
        {
            var next = random.Next(index, items.Length);
            var temp = items[index];
            items[index] = items[next];
            items[next] = temp;
        }
        return items;
    }
}
