using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using GestionDAB.Services.Interfaces;

namespace GestionDAB.Services.Implementations
{
    public class BanqueService : IBanqueService
    {
        private readonly IBanqueRepository _repo;

        public BanqueService(IBanqueRepository repo) => _repo = repo;

        public Task<IEnumerable<Banque>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Banque?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Banque?> GetWithComptesAsync(int id) => _repo.GetWithComptesAsync(id);
        public Task CreateAsync(Banque banque) => _repo.AddAsync(banque);
        public Task UpdateAsync(Banque banque) => _repo.UpdateAsync(banque);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<bool> CodeExistsAsync(int code) =>
            await _repo.GetByCodeAsync(code) != null;
    }
}