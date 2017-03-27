using System.Linq;
using System.Collections.Generic;

namespace CsEx
{
    public static class RandomEx
    {
        public static int ThreadSafeRange(int min, int max)
        {
            return SafeRandom.Range(min, max);
        }
    }
}
