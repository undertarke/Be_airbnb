using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface IDatPhongRepository : IRepository<DatPhong>
    {
    
    }

    public class DatPhongRepository : RepositoryBase<DatPhong>, IDatPhongRepository
    {
        public DatPhongRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

  
    }
}