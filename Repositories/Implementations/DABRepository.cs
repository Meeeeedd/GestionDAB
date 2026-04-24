using GestionDAB.Data;
using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionDAB.Repositories.Implementations
{
    public class DABRepository : BaseRepository<DAB>, IDABRepository
    {
        public DABRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<DAB>> GetEnServiceAsync() =>
            await _context.DABs
                .Where(d => d.EstEnService)
                .ToListAsync();

        public async Task<DAB?> GetByDABIdAsync(string dabId) =>
            await _context.DABs
                .FirstOrDefaultAsync(d => d.DABId == dabId);

        public async Task<DAB?> GetWithTransactionsAsync(int id) =>
            await _context.DABs
                .Include(d => d.Transactions)
                .FirstOrDefaultAsync(d => d.Id == id);
    }
}