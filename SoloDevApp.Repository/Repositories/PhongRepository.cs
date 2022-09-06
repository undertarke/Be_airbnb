using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface IPhongRepository : IRepository<Phong>
    {
    
    }

    public class PhongRepository : RepositoryBase<Phong>, IPhongRepository
    {
        public PhongRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

  
    }
}