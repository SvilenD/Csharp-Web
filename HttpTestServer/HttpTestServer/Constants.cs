namespace HttpTestServer
{
    public static class Constants
    {
        public const int Port = 12345;
        public const int MaxAge = 360;

        public const string ResultFileLocation = "../../../result.html";
        public const string ResponsePage = "../../../ResponsePage.html";
        public const string SessionIdPattern = "sid=[^\n]*\n";
        public const string RequesterStart = "--------------------START OF HTTP REQUESTER JOB!--------------------";
        public const string RequesterEnd = "---------------------END OF HTTP REQUESTER JOB!---------------------";
    }
}