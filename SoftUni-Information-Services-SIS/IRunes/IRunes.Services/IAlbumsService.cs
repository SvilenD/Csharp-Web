using IRunes.Models;
using System.Collections.Generic;

namespace IRunes.Services
{
    public interface IAlbumsService
    {
        void Create(string name, string cover);

        IEnumerable<Album> GetAll();

        Album GetDetails(string id);
    }
}
