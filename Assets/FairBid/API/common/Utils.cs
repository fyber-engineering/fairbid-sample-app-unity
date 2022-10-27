using System.Collections.Generic;
using System.Linq;

namespace Fyber
{
    class Utils
    {
        // Within Unity's .NET framework we don't have a stock solution for converting objets to Json so we need to implement a custom solution
        static public string DictToJson(Dictionary<string, string> dictionary)
        {
            var entries = dictionary.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, d.Value)
            );
            return "{" + string.Join(",", entries.ToArray()) + "}";
        }
    }
}