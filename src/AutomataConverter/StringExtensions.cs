namespace AutomataConverter
{
    public static class StringExtensions
    {
        /// <summary>
        /// Force all line-ending characters in the string to match the specified style
        /// </summary>
        /// <param name="input">The string to normalize</param>
        /// <param name="style">The style to force</param>
        /// <returns>a string with all line endings matching the specified style</returns>
        public static string NormalizeLineEndingsTo(this string input, LineEndingStyle style)
        {
            return style == LineEndingStyle.LF ? input.Replace("\r\n", "\n") : input.Replace("\n", "\r\n");
        }
    }
}