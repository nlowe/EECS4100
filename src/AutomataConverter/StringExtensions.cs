namespace AutomataConverter.Tests
{
    public static class StringExtensions
    {
        public static string NormalizeLineEndingsTo(this string input, LineEndingStyle style)
        {
            if(style == LineEndingStyle.LF) return input.Replace("\r\n", "\n");
            else return input.Replace("\n", "\r\n");
        }
    }
}