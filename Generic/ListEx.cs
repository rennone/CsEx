using System;
using System.Collections.Generic;

namespace CsEx
{
    public static class ListEx
    {
        public static int CountOrDefault<T>(this List<T> self)
        {
            return self == null ? 0 : self.Count;
        }

       /// <summary>
       /// 2条件ソート
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <typeparam name="TU1"></typeparam>
       /// <typeparam name="TU2"></typeparam>
       /// <param name="self"></param>
       /// <param name="selector1"></param>
       /// <param name="selector2"></param>
        public static void Sort<T, TU1, TU2>(this List<T> self, Func<T, TU1> selector1,
            Func<T, TU2> selector2) where TU1 : IComparable where TU2 : IComparable
        {
            self.Sort((x, y) =>
            {
                var result = selector1(x).CompareTo(selector1(y));
                return result != 0 ? result : selector2(x).CompareTo(selector2(y));
            });
        }

        /// <summary>
        /// 3条件ソート
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU1"></typeparam>
        /// <typeparam name="TU2"></typeparam>
        /// <typeparam name="TU3"></typeparam>
        /// <param name="self"></param>
        /// <param name="selector1"></param>
        /// <param name="selector2"></param>
        /// <param name="selector3"></param>
        public static void Sort<T, TU1, TU2, TU3>(this List<T> self, Func<T, TU1> selector1, Func<T, TU2> selector2, Func<T, TU3> selector3) 
            where TU1 : IComparable
            where TU2 : IComparable 
            where TU3 : IComparable
        {
            self.Sort((x, y) =>
            {
                var result = selector1(x).CompareTo(selector1(y));
                if (result != 0)
                    return result;

                result = selector2(x).CompareTo(selector2(y));
                return result != 0 ? result : selector3(x).CompareTo(selector3(y));
            });
        }
    }

}

