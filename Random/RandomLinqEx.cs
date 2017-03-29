using System;
using System.Collections.Generic;
using System.Linq;

namespace CsEx
{
    public static class RandomLinqEx
    {
        /// <summary>
        /// ランダムに一つを返す. 無い場合はデフォルト
        /// </summary>
        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> self)
        {
            return self.IsNullOrEmpty() ? default(TSource) : self.Random();
        }

        /// <summary>
        /// weightで指定した重みをもとに,ランダムに一つを返す. 無い場合はデフォルト
        /// </summary>
        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> self, Func<TSource, int> weight)
        {
            return self.IsNullOrEmpty() ? default(TSource) : self.Random();
        }

        /// <summary>
        /// ランダムに一つを返す. 無い場合は例外
        /// </summary>
        public static TSource Random<TSource>(this IEnumerable<TSource> self)
        {
            return self.ElementAt(SafeRandom.Range(0, self.Count()));
        }

        /// <summary>
        /// weightで指定した重みをもとに, ランダムに一つを返す. 無い場合は例外
        /// </summary>
        public static TSource Random<TSource>(this IEnumerable<TSource> self, Func<TSource, int> weight)
        {
            return self.ElementAt(SafeRandom.DiceToss(self.Select(weight)));
        }


    }
}

