using Domain.Entities;
using Domain.ViewModel.Colors;

namespace Application.Interfaces.Repositories
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        Task<IEnumerable<ColorVM>> GetAllColors();

        Task<IEnumerable<ColorVM>> GetColorById(int Id);
    }
}