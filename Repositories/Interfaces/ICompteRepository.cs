using GestionDAB.Models.Entities;

namespace GestionDAB.Repositories.Interfaces
{
    public interface ICompteRepository : IRepository<Compte>
    {
        Task<Compte?> GetByNumeroAsync(string numeroCompte);
        Task<IEnumerable<Compte>> GetByBanqueAsync(int banqueId);
        Task<IEnumerable<Compte>> GetByTypeAsync(TypeCompte type);
        Task<Compte?> GetWithTransactionsAsync(int id);
    }
}