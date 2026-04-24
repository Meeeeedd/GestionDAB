using GestionDAB.Models.Entities;

namespace GestionDAB.Repositories.Interfaces
{
    public interface IDABRepository : IRepository<DAB>
    {
        Task<IEnumerable<DAB>> GetEnServiceAsync();
        Task<DAB?> GetByDABIdAsync(string dabId);
        Task<DAB?> GetWithTransactionsAsync(int id);
    }
}