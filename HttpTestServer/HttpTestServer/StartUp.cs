﻿namespace HttpTestServer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;

    public class StartUp
    {
        private const int Port = 12345;

        public static async Task Main()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, Port);
            tcpListener.Start();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            //Task.Run(() => HttpRequesterAsync());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            // WebUtility.UrlDecode
            while (true)
            {
                // TCP / UDP
                TcpClient client = await tcpListener.AcceptTcpClientAsync();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ClientProcessor.ProcessClientAsync(client));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public async static Task HttpRequesterAsync()
        {
            Console.WriteLine("--------------------START OF HTTP REQUESTER JOB!--------------------");

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://softuni.bg");
            string result = await response.Content.ReadAsStringAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            File.WriteAllTextAsync("../../../result.html", result);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            Thread.Sleep(10000);

            Console.WriteLine(response);
            Console.WriteLine("---------------------END OF HTTP REQUESTER JOB!---------------------");
        }
    }
}