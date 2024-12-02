using Humanizer;
using static System.Net.Mime.MediaTypeNames;

namespace GatherUp.Utils
{
    public static class TextUtils
    {
        public static string Truncate(string text, int maxLength, string suffix = " (...)")
        {
            if (string.IsNullOrEmpty(text)) return text;

            return text.Truncate(maxLength, suffix);
        }
    }
}
