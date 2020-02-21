using IRunes.App.ViewModels.Albums;
using IRunes.App.ViewModels.Tracks;
using IRunes.Services;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumService;

        public AlbumsController(IAlbumsService albumService)
        {
            this.albumService = albumService;
        }

        public HttpResponse Create()
        {
            //if (!this.IsUserLoggedIn())
            //{
            //    return this.Redirect(Constants.LoginPath);
            //}

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AlbumCreateInputModel model)
        {
            //if (!this.IsUserLoggedIn())
            //{
            //    return this.Redirect(Constants.LoginPath);
            //}
            if (model?.Name.Length < 4 || model?.Name.Length > 20)
            {
                return this.Error(Constants.InvalidNameLength);
            }
            if (string.IsNullOrEmpty(model?.Cover))
            {
                return this.Error(Constants.InvalidNameLength);
            }

            this.albumService.Create(model.Name, model.Cover);

            return this.Redirect("/Albums/All");
        }

        public HttpResponse All()
        {
            //if (!this.IsUserLoggedIn())
            //{
            //    return this.Redirect(Constants.LoginPath);
            //}

            var albums = this.albumService.GetAll();
            var albumViewModels = new AllAlbumsViewModel
            {
                Albums = albums.Select(a => new AlbumViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price
                }).ToList()
            }; 

            return this.View(albumViewModels);
        }

        public HttpResponse Details(string id)
        {
            var album = this.albumService.GetDetails(id);
            var albumView = new AlbumDetailsViewModel
            {
                Id = id,
                Name = album.Name,
                Cover = album.Cover,
                Price = album.Price,
                Tracks = album.Tracks.Select(t => new TrackInfoViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };
            return this.View(albumView);
        }
    }
}
