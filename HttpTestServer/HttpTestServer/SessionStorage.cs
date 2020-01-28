namespace HttpTestServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class SessionStorage
    {
        private const string SessionIdPattern = "sid=[^\n]*\n";
        private const string SessionMatch = @"^(?<sid>(.+))( => )(?<count>(\d)+)$";
        private const string FileLocation = "../../../SessionStore.txt";
        private static Dictionary<string, int> SessionData = GetData();

        public static KeyValuePair<string, int> GetSessionInfo(string requestInfo)
        {
            string sid = Regex.Match(requestInfo.ToString(), SessionIdPattern).Value?.Replace("sid=", string.Empty).Trim();
            int count = 1;
            if (SessionData.ContainsKey(sid))
            {
                count = ++SessionData[sid];

                File.Delete(FileLocation);
                StringBuilder stringInfo = new StringBuilder();

                Parallel.ForEach(SessionData, (line) =>
                {
                    stringInfo.AppendLine($"{line.Key} - {line.Value}");
                });

                File.WriteAllTextAsync(FileLocation, stringInfo.ToString());
            }
            else
            {
                string newSid = Guid.NewGuid().ToString();
                SessionData[newSid] = count;
                sid = newSid;
                File.AppendAllText(FileLocation, $"{Environment.NewLine}{sid} => {count}");
            }

            return new KeyValuePair<string, int>(sid, count);
        }

        private static Dictionary<string, int> GetData()
        {
            var data = File.ReadAllLines(FileLocation);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var line in data)
            {
                if (Regex.IsMatch(line, SessionMatch))
                {
                    string sid = Regex.Match(line, SessionMatch).Groups["sid"].Value;
                    int count = int.Parse(Regex.Match(line, SessionMatch).Groups["count"].Value);
                    dict.Add(sid, count);
                }
            }
            return dict;
        }
    }
}