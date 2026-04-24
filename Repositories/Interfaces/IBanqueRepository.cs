using GestionDAB.Models.Entities;

namespace GestionDAB.Repositories.Interfaces
{
    public interface IBanqueRepository : IRepository<Banque>
    {
        Task<Banque?> GetByCodeAsync(int code);
        Task<IEnumerable<Banque>> GetByVilleAsync(string ville);
        Task<Banque?> GetWithComptesAsync(int id);
    }
}