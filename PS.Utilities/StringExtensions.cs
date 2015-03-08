using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace PS.Utilities
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static int CountChars(this string post, char character, bool ignoreCase = true)
        {
            return new Regex(Regex.Escape(new string(character, 1)), ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None).Matches(post).Count;
        }

        public static int CountWordsThatEndWithChar(this string post, char character)
        {
            return Regex.Matches(post, string.Format(@"\S*{0}\b", character), RegexOptions.IgnoreCase).Count;
        }

        public static int CountSentencesWithMaxWords(this string post, int minWords, char delimiter)
        {
            var paragraphs = Regex.Matches(post, string.Format(@"(?<={0}|^)[^{0}]*{0}", delimiter)).Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            return paragraphs.Count(x => Regex.Matches(x, "\\w+").Count > minWords);
        }

        public static int CountParagraphs(this string post, char delimiter)
        {
            return Regex.Matches(post, @"\w+" + delimiter, RegexOptions.IgnoreCase).Count;
        }

        public static int CountCharsExcept(this string post, char character)
        {
            /* \w summarizes this pattern, but it considers underscores*/
            var pattern = "[abcdefghijklmñnopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789]";

            pattern = Regex.Replace(pattern, character.ToString(CultureInfo.InvariantCulture), string.Empty, RegexOptions.IgnoreCase);

            return Regex.Matches(post, pattern, RegexOptions.IgnoreCase).Count;
        }

    }
}
