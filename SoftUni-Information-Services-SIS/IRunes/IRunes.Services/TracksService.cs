using IRunes.Data;
using IRunes.Models;
using System.Linq;

namespace IRunes.Services
{
    public class TracksService : ITracksService
    {
        private readonly RunesDbContext db;

        public TracksService(RunesDbContext db)
        {
            this.db = db;
        }

        public void Create(string albumId, string name, string link, decimal price)
        {
            var track = new Track
            {
                AlbumId = albumId,
                Name = name,
                Link = link,
                Price = price,
            };

            this.db.Tracks.Add(track);

            var allTrackPricesSum = this.db.Tracks
                .Where(x => x.AlbumId == albumId)
                .Sum(x => x.Price) + price;
            var album = this.db.Albums.Find(albumId);
            album.Price = allTrackPricesSum * 0.87M;

            this.db.SaveChanges();
        }

        public Track GetDetails(string trackId) => db.Tracks.Find(trackId);
    }
}
