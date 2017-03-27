using System;
using System.Collections.Generic;
using System.Linq;

namespace CsEx
{
    /// <summary>
    /// スレッドセーフなランダム関数
    /// https://blogs.msdn.microsoft.com/pfxteam/2009/02/19/getting-random-numbers-in-a-thread-safe-way/
    /// </summary>
    public class SafeRandom
    {
        private static readonly System.Random global_ = new System.Random();

        // 各スレッドごとに独立して作られる
        [ThreadStatic]
        private static Random local_;

        private static Random Local
        {
            get
            {
                if (local_ != null)
                    return local_;
                int seed;
                lock (global_)
                {
                    seed = global_.Next();
                }

                local_ = new Random(seed);

                return local_;
            }
        }

        /// <summary>
        /// 0以上の整数を返す
        /// </summary>
        /// <returns></returns>
        public static int Next() { return Local.Next(); }

        /// <summary>
        /// [inclusive]0と1.0[inclusive]の間の乱数を返す。
        /// </summary>
        /// <returns></returns>
        public static double NextDouble() { return Local.NextDouble(); }

        /// <summary>
        ///  return [inclusive]min ~ max[exclusive]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Range(int min, int max)
        {
            return Local.Next(min, max);
        }

        /// <summary>
        ///  return [inclusive]min ~ max[inclusive]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Range(float min, float max)
        {
            return (float)(NextDouble() * (min - max) + min);
        }

        /// <summary>
        /// weightsがダイスの面が出る重みを表し,表が出た面のインデックスを返す.
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static int DiceToss(params int[] weights)
        {
            // 重みの全合計値から乱数発生
            // 0 ~ sum-1
            var value = Range(0, weights.Sum());

            var total = 0;
            foreach (var item in weights.Select((v, i) => new { v, i }))
            {
                // それまでの合計値
                total += item.v;

                // 超えた瞬間その目が答え
                if (total > value)
                    return item.i;
            }

            // パラメータが無いときに来る
            //DebugLogger.LogWarning("DiceToss : no weight parameter", DebugLogger.Tag.Global);
            return weights.Length - 1;
        }

        public static int DiceToss(IEnumerable<int> weights)
        {
            // 重みの全合計値から乱数発生
            // 0 ~ sum-1
            var value = Range(0, weights.Sum());

            int total = 0;
            foreach (var item in weights.Select((v, i) => new { v, i }))
            {
                // それまでの合計値
                total += item.v;

                // 超えた瞬間その目が答え
                if (total > value)
                    return item.i;
            }

            // パラメータが無いときに来る
            //DebugLogger.LogWarning("DiceToss : no weight parameter", DebugLogger.Tag.Global);
            return weights.Count() - 1;
        }

        /// <summary>
        /// DiceToss(weights)をnum回実行したとき, 各面の出た回数を配列で返す.
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static int[] DiceToss(int num, params int[] weights)
        {
            if (num <= 0 || weights.Length == 0)
                return new int[weights.Length];

            var ret = new int[weights.Length];

            for (var i = 0; i < num; ++i)
                ret[DiceToss(weights)] += 1;

            return ret;
        }

        /// <summary>
        /// 成功率suucess%において成功したらtrueを返す。
        /// </summary>
        /// <param name="successRatePercent"></param>
        /// <returns></returns>
        public static bool CoinToss(int successRatePercent)
        {
            return Range(0, 100) < successRatePercent;
        }

        /// <summary>
        /// 成功率suucess%において成功したらtrueを返す。
        /// </summary>
        /// <param name="successRatePercent"></param>
        /// <returns></returns>
        public static bool CoinToss(float successRatePercent)
        {
            // floatのRangeはmax値を含むため, -Mathf.Epsilonしている
            return Range(0f, 100f - MathEx.Epsilon) < successRatePercent;
        }
    }
}
