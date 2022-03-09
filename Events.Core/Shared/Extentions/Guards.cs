namespace Events.Core.Shared.Extentions
{
    public static class Guards
    {
        public static string ThrowIfNullOrWhiteSpace(this string text, string parameterName)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"Parameter {parameterName} is null or empty.");
            }
            return text;
        }

        public static T ThrowIfNull<T>(this T obj, string parameterName)
        {
            return obj ?? throw new ArgumentNullException($"Parameter {parameterName} is null.");
        }

    }
}
