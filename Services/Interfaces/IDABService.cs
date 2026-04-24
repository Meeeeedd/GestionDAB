using GestionDAB.Models.Entities;

namespace GestionDAB.Services.Interfaces
{
    public interface IDABService
    {
        Task<IEnumerable<DAB>> GetAllAsync();
        Task<DAB?> GetByIdAsync(int id);
        Task<IEnumerable<DAB>> GetEnServiceAsync();
        Task<DAB?> GetWithTransactionsAsync(int id);
        Task CreateAsync(DAB dab);
        Task UpdateAsync(DAB dab);
        Task DeleteAsync(int id);
    }
}