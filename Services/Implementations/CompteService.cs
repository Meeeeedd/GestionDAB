using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using GestionDAB.Services.Interfaces;

namespace GestionDAB.Services.Implementations
{
    public class CompteService : ICompteService
    {
        private readonly ICompteRepository _repo;

        public CompteService(ICompteRepository repo) => _repo = repo;

        public Task<IEnumerable<Compte>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Compte?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Compte?> GetByNumeroAsync(string num) => _repo.GetByNumeroAsync(num);
        public Task<Compte?> GetWithTransactionsAsync(int id) => _repo.GetWithTransactionsAsync(id);
        public Task<IEnumerable<Compte>> GetByBanqueAsync(int banqueId) => _repo.GetByBanqueAsync(banqueId);
        public Task CreateAsync(Compte compte) => _repo.AddAsync(compte);
        public Task UpdateAsync(Compte compte) => _repo.UpdateAsync(compte);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<bool> NumeroExistsAsync(string numeroCompte) =>
            await _repo.GetByNumeroAsync(numeroCompte) != null;
    }
}