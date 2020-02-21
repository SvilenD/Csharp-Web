using IRunes.App.ViewModels.Tracks;
using IRunes.Services;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            var viewModel = new TrackCreateViewModel { AlbumId = albumId };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string albumId, string name, string link, decimal price)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }
            if (name.Length < 4 || name.Length > 20)
            {
                return this.Error(Constants.InvalidNameLength);
            }
            if (string.IsNullOrEmpty(link))
            {
                return this.Error(Constants.InvalidLink);
            }
            if (price < 0)
            {
                return this.Error(Constants.NegativePrice); 
            }

            this.tracksService.Create(albumId, name, link, price);

            return this.Redirect("/Albums/Details?id=" + albumId);
        }

        public HttpResponse Details(string trackId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            var track = this.tracksService.GetDetails(trackId);
            if (track == null)
            {
                return this.Error("Track not found.");
            }
            var viewModel = new TrackDetailsViewModel
            {
                Name = track.Name,
                Link = track.Link,
                AlbumId = track.AlbumId,
                Price = track.Price
            };

            return this.View(viewModel);
        }
    }
}