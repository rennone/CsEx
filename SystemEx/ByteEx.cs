namespace CsEx
{
    public static class ByteEx
    {

        public static byte LeftShift(this byte self, bool fill)
        {
            return (byte) ((self << 1) | (fill ? 1 : 0));
        }

        public static byte RightShift(this byte self, bool fill)
        {
            return (byte) ((self >> 1) | (fill ? 1 << 7 : 0));
        }

        public static byte LeftShift(this byte self, byte amount, byte fill)
        {
            return (byte) ((self << amount) | (fill >> amount));
        }

        public static byte RightShift(this byte self, byte amount, byte fill)
        {
            return (byte) ((self >> amount) | fill << amount);
        }
    }
}

