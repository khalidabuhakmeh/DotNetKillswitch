namespace DotNetKillswitch
{
    public static class Constants
    {
        public const string Prefix = ".kss";
        public const string CssContentType = "text/css";
        public const string TextContentType = "text/plain";
        public const string Css = "body { background: black !important; } body * { display: none !important; }";
        public const string CssLink = "<link href=\"{0}{1}.kss\" type=\"text/css\" rel=\"stylesheet\" />";
        public const string DatabasePath = "App_Data\\Killswitch.db3";
    }
}