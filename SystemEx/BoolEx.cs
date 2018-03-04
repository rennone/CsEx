namespace CsEx
{
    public static class BoolEx
    {
        /// <summary>
        /// trueだと1,falseだと0を返す
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int ToInt(this bool self)
        {
            return self ? 1 : 0;
        }
    }
}
