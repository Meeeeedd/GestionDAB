using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using GestionDAB.Services.Interfaces;

namespace GestionDAB.Services.Implementations
{
    public class DABService : IDABService
    {
        private readonly IDABRepository _repo;

        public DABService(IDABRepository repo) => _repo = repo;

        public Task<IEnumerable<DAB>> GetAllAsync() => _repo.GetAllAsync();
        public Task<DAB?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<IEnumerable<DAB>> GetEnServiceAsync() => _repo.GetEnServiceAsync();
        public Task<DAB?> GetWithTransactionsAsync(int id) => _repo.GetWithTransactionsAsync(id);
        public Task CreateAsync(DAB dab) => _repo.AddAsync(dab);
        public Task UpdateAsync(DAB dab) => _repo.UpdateAsync(dab);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}