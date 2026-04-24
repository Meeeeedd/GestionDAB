using GestionDAB.Data;
using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionDAB.Repositories.Implementations
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Transaction>> GetAllWithDetailsAsync() =>
            await _context.Transactions
                .Include(t => t.Compte)
                .Include(t => t.DAB)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

        public async Task<IEnumerable<Transaction>> GetByCompteAsync(int compteId) =>
            await _context.Transactions
                .Include(t => t.Compte)
                .Include(t => t.DAB)
                .Where(t => t.CompteId == compteId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

        public async Task<IEnumerable<Transaction>> GetByDABAsync(int dabId) =>
            await _context.Transactions
                .Include(t => t.Compte)
                .Include(t => t.DAB)
                .Where(t => t.DABId == dabId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

        public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(
            DateTime from, DateTime to) =>
            await _context.Transactions
                .Include(t => t.Compte)
                .Include(t => t.DAB)
                .Where(t => t.Date >= from && t.Date <= to)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

        public async Task<IEnumerable<Transaction>> GetByTypeAsync(string type) =>
            await _context.Transactions
                .Include(t => t.Compte)
                .Include(t => t.DAB)
                .Where(t => t.Type == type)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

        public async Task<IEnumerable<Transaction>> SearchAsync(
            string? libelle, DateTime? from, DateTime? to,
            string? type, double? montantMin, double? montantMax)
        {
            var query = _context.Transactions
                .Include(t => t.Compte)
                .Include(t => t.DAB)
                .AsQueryable();

            if (!string.IsNullOrEmpty(libelle))
                query = query.Where(t =>
                    t.Libelle.ToLower().Contains(libelle.ToLower()));

            if (from.HasValue)
                query = query.Where(t => t.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(t => t.Date <= to.Value);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(t => t.Type == type);

            if (montantMin.HasValue)
                query = query.Where(t => t.Montant >= montantMin.Value);

            if (montantMax.HasValue)
                query = query.Where(t => t.Montant <= montantMax.Value);

            return await query
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }
}