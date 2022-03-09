namespace Events.Web.Options
{
    public class JwtSettings
    {
        public static string Name => "JwtSettings";
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secret { get; set; }
        public string Expires { get; set; }
    }
}
