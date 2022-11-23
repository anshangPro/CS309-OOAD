using System;
using System.Collections.Generic;
using System.Linq;

namespace Util
{
    public static class ListUtil
    {
        public static List<T> Shuffle<T>(List<T> list)
        {
            Random random = new Random();
            List<T> ret = list.OrderBy(x => random.Next()).ToList();
            return ret;
        }
    }
}