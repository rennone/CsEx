namespace CsEx
{
    public static class ArrayEx
    {
        public static int LengthOrDefault<T>(this T[] self)
        {
            return self == null ? 0 : self.Length;
        }
    }

}