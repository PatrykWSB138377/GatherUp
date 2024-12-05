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


        public static string FormBasedOnCount(int count, string singular, string plural, string pluralBig)
        {
            string form = singular;
            if (count == 1)
            {
                form = singular;
            }
            else if (count >= 2 && count <= 4)
            {
                form = plural;
            }
            else
            {
                form = pluralBig;
            }

            return $"{count} {form}";
        }
    }
}
