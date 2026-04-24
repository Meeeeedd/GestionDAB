using GestionDAB.Models.Entities;

namespace GestionDAB.Services.Interfaces
{
    public interface ICompteService
    {
        Task<IEnumerable<Compte>> GetAllAsync();
        Task<Compte?> GetByIdAsync(int id);
        Task<Compte?> GetByNumeroAsync(string numeroCompte);
        Task<Compte?> GetWithTransactionsAsync(int id);
        Task<IEnumerable<Compte>> GetByBanqueAsync(int banqueId);
        Task CreateAsync(Compte compte);
        Task UpdateAsync(Compte compte);
        Task DeleteAsync(int id);
        Task<bool> NumeroExistsAsync(string numeroCompte);
    }
}