namespace SimpleMVC.App.MVC.Extentions
{
    public static class StringExtentions
    {
        public static string ToUpperFirst(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
