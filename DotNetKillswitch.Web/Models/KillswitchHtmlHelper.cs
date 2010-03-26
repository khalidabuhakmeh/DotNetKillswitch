using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetKillswitch.Web.Models
{
    public static class KillswitchHtmlHelper
    {
        public static string BlaklistDate(this HtmlHelper helper, DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToLongDateString() : "Never";
        }
    }
}