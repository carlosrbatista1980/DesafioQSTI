using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DesafioQSTI.Models
{
    public class SessionModel : PageModel
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";
        const string SessionKeyTime = "_Time";

        public string SessionInfo_Name { get; private set; }
        public string SessionInfo_Age { get; private set; }
        public string SessionInfo_CurrentTime { get; private set; }
        public string SessionInfo_SessionTime { get; private set; }
        public string SessionInfo_MiddlewareValue { get; private set; }

        public void OnGet()
        {
            // Requires: using Microsoft.AspNetCore.Http;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, "The Doctor");
                HttpContext.Session.SetInt32(SessionKeyAge, 773);
            }

            var name = HttpContext.Session.GetString(SessionKeyName);
            var age = HttpContext.Session.GetInt32(SessionKeyAge);
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}


    