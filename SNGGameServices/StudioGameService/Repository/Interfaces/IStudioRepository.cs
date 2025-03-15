using Library.GenericRepository.Interfaces;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IStudioRepository : IGenericRepository<Studio, int>
    {
    }
}
