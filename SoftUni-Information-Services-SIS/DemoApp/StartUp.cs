using DemoApp.Data;
using DemoApp.Data.Models;
using SIS.HTTP;
using SIS.HTTP.Enums;
using SIS.HTTP.Response;
using SIS.MvcFramework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DemoApp
{
    public class StartUp : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            routeTable.Add(new Route(HttpMethodType.Get, "/", Index));
            routeTable.Add(new Route(HttpMethodType.Post, "/Tweets/Create", CreateTweet));
            routeTable.Add(new Route(HttpMethodType.Get, "/favicon.ico", FavIcon));
        }

        public void ConfigureServices()
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }

        // HomeController
        public static HttpResponse Index(HttpRequest request)
        {
            var username = request.SessionData.ContainsKey("Username") ? request.SessionData["Username"] : "Anonymous";

            using var db = new ApplicationDbContext();
            var tweets = db.Tweets.Select(x => new
            {
                x.CreatedOn,
                x.Creator,
                x.Content,
            }).ToList();

            StringBuilder html = new StringBuilder();
            html.Append("<h1>Last Tweets:</h1>");
            if (tweets.Any())
            {
                html.Append("<p><table><tr><th>Date/UTC Time</th><th>Creator</th><th>Content</th></tr>");
                foreach (var tweet in tweets.OrderByDescending(t => t.CreatedOn).Take(20))
                {
                    html.Append($"<tr><td>{tweet.CreatedOn}</td><td>{tweet.Creator}</td><td>{tweet.Content}</td></tr>");
                }
                html.Append("</table><p>");
                html.Append("<hr>");
            }

            html.Append("<article><h3>Create your new Tweet</h3></article>");
            html.Append($"<form action='/Tweets/Create' method='post'><input name='creator' placeholder='Your Name:'/><br /><textarea name='tweetName' placeholder='Enter Tweet Here:'></textarea><br /><input type='submit' /></form>");

            return new HtmlResponse(html.ToString());
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var byteContent = File.ReadAllBytes("wwwroot/favicon.ico");
            return new FileResponse(byteContent, "image/x-icon");
        }

        // /Tweets => Index
        // /Tweets/Create => Create
        private static HttpResponse CreateTweet(HttpRequest request)
        {
            using var db = new ApplicationDbContext();
            db.Tweets.Add(new Tweet
            {
                CreatedOn = DateTime.UtcNow,
                Creator = request.FormData["creator"],
                Content = request.FormData["tweetName"],
            });
            db.SaveChanges();

            return new RedirectResponse("/");
        }
    }
}