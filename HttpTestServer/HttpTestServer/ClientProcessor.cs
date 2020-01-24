namespace HttpTestServer
{
    using System;
    using System.Text;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class ClientProcessor
    {
        private const string NewLine = "\r\n";
        private readonly TcpClient client;

        public ClientProcessor(TcpClient client)
        {
            this.client = client;
            Task.Run(() => this.ProcessClientAsync());
        }

        private async Task ProcessClientAsync()
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

                string responseBody = @"<form action='/Account/Login' method='post'>
                                            <label for='username'> Username: </label>
                                            <input type=text name='username' />
                                            <label for='password'>Password: </label>
                                            <input type=password name='pasword' />
                                            <input type=date name='date' />
                                            <input type=submit value='Login' />
                                            </form>";
                string response = "HTTP/1.1 200 OK" + NewLine + //"HTTP/1.1 301 Moved" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                  //"Location: https://google.com" + NewLine +
                                  //"Content-Disposition: attachment; filename=index.html" + NewLine +
                                  "Server: MyCustomServer/1.0" + NewLine +
                                  $"Content-Length: {responseBody.Length}" + NewLine + NewLine +
                                  responseBody;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }
    }
}