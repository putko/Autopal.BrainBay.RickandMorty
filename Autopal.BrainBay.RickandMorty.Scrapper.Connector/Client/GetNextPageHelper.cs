using System;
using System.Text.RegularExpressions;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client
{
    internal static class GetNextPageHelper
    {
        private static readonly Regex PagePattern = new("page=(?<pagenr>[0-9]+)");

        public static int GetNextPageNumber(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return -1;

            var result = PagePattern.Match(url).Groups["pagenr"].Value;

            if (string.IsNullOrEmpty(result))
                return -1;

            return Convert.ToInt32(result);
        }
    }
}