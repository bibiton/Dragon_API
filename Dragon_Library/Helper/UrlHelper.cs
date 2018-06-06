using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Dragon_Library.Helper
{
    public class UrlHelper
    {
        public static string testResultUrl = ConfigurationManager.AppSettings["testResultUrl"];
        public static string shareUrl = ConfigurationManager.AppSettings["shareUrl"];
        public static string cdUrl = ConfigurationManager.AppSettings["StorageUrl"] + ConfigurationManager.AppSettings["cdUrl"];
    }
}