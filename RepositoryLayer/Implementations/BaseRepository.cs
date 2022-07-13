using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
namespace RepositoryLayer.Implementations
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IConfiguration _config;

        public BaseRepository (IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString ()
        {
            return _config["DBSettings:ConnectionString"];
        }
    }
}
