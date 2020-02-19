using IRunes.Data;
using IRunes.Models;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.Services
{
    public class AlbumsService : IAlbumService
    {
        private readonly RunesDbContext db;

        public AlbumsService(RunesDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string cover)
        {
            var album = new Album { Name = name, Cover = cover };

            this.db.Add(album);
            this.db.SaveChanges();
        }

        public IEnumerable<Album> GetAll() => this.db.Albums.ToList();

        public Album GetDetails(string id) => this.db.Albums.FirstOrDefault(a => a.Id == id);
    }
}