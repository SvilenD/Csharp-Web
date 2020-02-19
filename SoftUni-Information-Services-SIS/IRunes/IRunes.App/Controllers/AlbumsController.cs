using IRunes.App.ViewModels.Albums;
using IRunes.Services;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumsController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AlbumCreateInputModel model)
        {
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
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

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
            return this.View(album);
        }
    }
}
