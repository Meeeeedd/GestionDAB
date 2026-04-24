using GestionDAB.Data;
using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionDAB.Repositories.Implementations
{
    public class CompteRepository : BaseRepository<Compte>, ICompteRepository
    {
        public CompteRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Compte>> GetAllAsync() =>
            await _context.Comptes
                .Include(c => c.Banque)
                .ToListAsync();

        public async Task<Compte?> GetByNumeroAsync(string numeroCompte) =>
            await _context.Comptes
                .Include(c => c.Banque)
                .FirstOrDefaultAsync(c => c.NumeroCompte == numeroCompte);

        public async Task<IEnumerable<Compte>> GetByBanqueAsync(int banqueId) =>
            await _context.Comptes
                .Include(c => c.Banque)
                .Where(c => c.BanqueId == banqueId)
                .ToListAsync();

        public async Task<IEnumerable<Compte>> GetByTypeAsync(TypeCompte type) =>
            await _context.Comptes
                .Include(c => c.Banque)
                .Where(c => c.Type == type)
                .ToListAsync();

        public async Task<Compte?> GetWithTransactionsAsync(int id) =>
            await _context.Comptes
                .Include(c => c.Banque)
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}