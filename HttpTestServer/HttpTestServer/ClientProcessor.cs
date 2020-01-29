namespace HttpTestServer
{
    using System;
    using System.Text;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using System.IO;
    using System.Collections.Generic;

    public static class ClientProcessor
    {
        private const string NewLine = "\r\n";

        public static async Task ProcessClientAsync(TcpClient client)
        {
            using (NetworkStream networkStream = client.GetStream())
            {
                StringBuilder requestInfo = new StringBuilder();

                while (networkStream.DataAvailable && networkStream.CanRead)
                {
                    byte[] requestBytes = new byte[256];

                    int readBytes = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
                    requestInfo.Append(Encoding.UTF8.GetString(requestBytes, 0, readBytes));
                }

                Console.WriteLine(new string('=', 70));
                Console.WriteLine(requestInfo);

                KeyValuePair<string,int> sessionInfo = SessionStorage.GetSessionInfo(requestInfo.ToString());

                string responseBody = await File.ReadAllTextAsync("../../../ResponsePage.html");
                responseBody += $"<h3> Session Id: {sessionInfo.Key} => Connections Count = {sessionInfo.Value}</h3>";

                string response = "HTTP/1.1 200 OK" + NewLine + //"HTTP/1.1 301 Moved" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                  $"Set-Cookie: sid={sessionInfo.Key}; Path=/; " +
                                  $"Max-Age=600; HttpOnly; SameSite=Strict" + NewLine + //Secure - for https
                                  //"Location: https://google.com" + NewLine +
                                  //"Content-Disposition: attachment; filename=response.html" + NewLine +
                                  "Server: MyCustomServer/1.0" + NewLine +
                                  $"Content-Length: {responseBody.Length}" + NewLine + NewLine
                                  + responseBody + NewLine;

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }

    }
}