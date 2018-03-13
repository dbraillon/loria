using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Loria.Modules.Reminder
{
    public static class TimeParser
    {
        public static TimeSpan Parse(string str)
        {
            var formatKeys = new[] { @"s\s", @"m\m", @"h\h", @"d\d" };
            var formats = formatKeys.GetVariations().Select(v => string.Join(" ", v)).ToArray();

            return TimeSpan.ParseExact(str, formats, CultureInfo.CurrentCulture);
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(
                    t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 })
                );
        }

        public static IEnumerable<IEnumerable<T>> GetVariations<T>(this IEnumerable<T> list)
        {
            var length = list.Count();
            if (length == 0) return new[] { list };

            var sizes = Enumerable.Range(1, length);
            return sizes.SelectMany(size => list.GetPermutations(size));
        }
    }
}
