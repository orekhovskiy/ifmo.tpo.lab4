using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Services
{
    public static class UrlService
    {
        public static string GetHubUrl() => "https://localhost:44308/messages/";
        public static string GetUserControllerUrl() => "https://localhost:44308/api/user/";
    }
}
