using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface IViTriRepository : IRepository<ViTri>
    {
    
    }

    public class ViTriRepository : RepositoryBase<ViTri>, IViTriRepository
    {
        public ViTriRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

  
    }
}