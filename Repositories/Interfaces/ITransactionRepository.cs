using GestionDAB.Models.Entities;

namespace GestionDAB.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByCompteAsync(int compteId);
        Task<IEnumerable<Transaction>> GetByDABAsync(int dabId);
        Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<IEnumerable<Transaction>> GetByTypeAsync(string type);
        Task<IEnumerable<Transaction>> SearchAsync(string? libelle, DateTime? from, DateTime? to, string? type, double? montantMin, double? montantMax);
        Task<IEnumerable<Transaction>> GetAllWithDetailsAsync();
    }
}