using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementalWords
{
    public static class Extensions
    {
        public static KeyValuePair<string, string> GetElement(this Dictionary<string, string> dict, string? input)
        {
            return dict.FirstOrDefault(x => string.Equals(x.Key, input, StringComparison.OrdinalIgnoreCase));
        }
    }
}
