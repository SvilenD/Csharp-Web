using IRunes.Models;
using System;
using System.Collections.Generic;

namespace IRunes.Services
{
    public interface IAlbumService
    {
        void Create(string name, string cover);

        IEnumerable<T> GetAll<T>(Func<Album, T> selectFunc);
    }
}
