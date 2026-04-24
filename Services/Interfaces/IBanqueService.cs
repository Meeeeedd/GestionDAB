using GestionDAB.Models.Entities;

namespace GestionDAB.Services.Interfaces
{
    public interface IBanqueService
    {
        Task<IEnumerable<Banque>> GetAllAsync();
        Task<Banque?> GetByIdAsync(int id);
        Task<Banque?> GetWithComptesAsync(int id);
        Task CreateAsync(Banque banque);
        Task UpdateAsync(Banque banque);
        Task DeleteAsync(int id);
        Task<bool> CodeExistsAsync(int code);
    }
}