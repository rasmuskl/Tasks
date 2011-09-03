using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Tasks.App.Models
{
    public static class HtmlExtensions
    {
        public static string StripHtml(this HtmlHelper helper, string htmlString)
        {
            htmlString = Regex.Replace(htmlString, @"</?[a-zA-Z]+\s*/?>", " ");
            htmlString = Regex.Replace(htmlString, @"\s[2,]", " ");

            return htmlString.Trim();
        }

    }
}