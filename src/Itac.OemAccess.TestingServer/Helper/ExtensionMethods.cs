using System;
using System.Collections.Generic;
using System.Text;

namespace Itac.OemAccess.TestingServer.Helper
{
    public static class ExtensionMethods
    {
        public static string ToString(this Dictionary<string, string> source, string keyValueSeparator, string sequenceSeparator)
        {
            if (source == null)
                throw new ArgumentException("Parameter source can not be null.");

            var str = new StringBuilder();
            foreach (var keyvaluepair in source)
                str.Append(string.Format("{0}{1}{2}{3}", keyvaluepair.Key, keyValueSeparator, keyvaluepair.Value, sequenceSeparator));
            var retval = str.ToString();
            return retval.Substring(0, retval.Length - sequenceSeparator.Length); //remove last  seq_separator
        }
        public static string Print(this Dictionary<string, string> source)
        {
            var keyValueSeparator = ":";
            var sequenceSeparator = ", ";

            if (source == null)
                throw new ArgumentException("Parameter source can not be null.");

            var str = new StringBuilder();
            foreach (var keyvaluepair in source)
                str.Append(string.Format("{0}{1}{2}{3}", keyvaluepair.Key, keyValueSeparator, keyvaluepair.Value, sequenceSeparator));
            var retval = str.ToString();
            return retval.Substring(0, retval.Length - sequenceSeparator.Length); //remove last  seq_separator
        }

    }

}
