using GestionDAB.Data;
using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionDAB.Repositories.Implementations
{
    public class BanqueRepository : BaseRepository<Banque>, IBanqueRepository
    {
        public BanqueRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Banque?> GetByCodeAsync(int code) =>
            await _context.Banques
                .FirstOrDefaultAsync(b => b.Code == code);

        public async Task<IEnumerable<Banque>> GetByVilleAsync(string ville) =>
            await _context.Banques
                .Where(b => b.Ville.ToLower().Contains(ville.ToLower()))
                .ToListAsync();

        public async Task<Banque?> GetWithComptesAsync(int id) =>
            await _context.Banques
                .Include(b => b.Comptes)
                .FirstOrDefaultAsync(b => b.Id == id);
    }
}