using System.Text;

namespace Juce.TweenComponent.Utils
{
    public static class StringUtils
    {
        public static string FirstCharToUpper(this string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder(str);

            stringBuilder[0] = char.ToUpper(stringBuilder[0]);

            return stringBuilder.ToString();
        }
    }
}
