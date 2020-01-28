namespace HttpTestServer
{
    using HttpTestServer.Data;
    using HttpTestServer.Data.Models;

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    //using Microsoft.EntityFrameworkCore;

    public static class SessionStorage
    {
        public static KeyValuePair<string, int> GetSessionInfo(string requestInfo)
        {
            using HttpTestServerContext db = new HttpTestServerContext();
            //db.Database.Migrate();

            string sid = Regex.Match(requestInfo.ToString(), 
                Constants.SessionIdPattern).Value?.Replace("sid=", string.Empty).Trim();

            var session = db.Sessions
                .Where(s => s.IsExpired == false)
                .FirstOrDefault(s => s.Id == sid);

            if (session == null)
            {
                sid = Guid.NewGuid().ToString();
                session = new Session()
                {
                    Id = sid,
                    Count = 0
                };
                db.Sessions.Add(session);
            }

            session.Count++;
            session.LastLogin = DateTime.UtcNow;

            SetExpired(db);
            db.SaveChangesAsync();

            return new KeyValuePair<string, int>(session.Id, session.Count);
        }

        private static void SetExpired(HttpTestServerContext db)
        {
            db.Sessions
                .Where(s => s.LastLogin > DateTime.UtcNow.AddSeconds(Constants.MaxAge))
                .ToList().ForEach(s => s.IsExpired = true);
        }
    }
}