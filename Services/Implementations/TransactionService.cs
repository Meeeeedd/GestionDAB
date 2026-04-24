using GestionDAB.Models.Entities;
using GestionDAB.Repositories.Interfaces;
using GestionDAB.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GestionDAB.Data;

namespace GestionDAB.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly ICompteRepository _compteRepo;
        private readonly ApplicationDbContext _context;

        public TransactionService(
            ITransactionRepository repo,
            ICompteRepository compteRepo,
            ApplicationDbContext context)
        {
            _repo = repo;
            _compteRepo = compteRepo;
            _context = context;
        }

        public Task<IEnumerable<Transaction>> GetAllAsync() =>
            _repo.GetAllWithDetailsAsync();

        public Task<Transaction?> GetByIdAsync(int id) =>
            _repo.GetByIdAsync(id);

        public Task<IEnumerable<Transaction>> GetByCompteAsync(int compteId) =>
            _repo.GetByCompteAsync(compteId);

        public Task<IEnumerable<Transaction>> SearchAsync(
            string? libelle, DateTime? from, DateTime? to,
            string? type, double? montantMin, double? montantMax) =>
            _repo.SearchAsync(libelle, from, to, type, montantMin, montantMax);

        public async Task<bool> EffectuerRetraitAsync(TransactionRetrait retrait)
        {
            var compte = await _compteRepo.GetByIdAsync(retrait.CompteId);
            if (compte == null || compte.Solde < retrait.Montant)
                return false;

            compte.Solde -= retrait.Montant;
            await _compteRepo.UpdateAsync(compte);
            await _repo.AddAsync(retrait);
            return true;
        }

        public async Task<bool> EffectuerTransfertAsync(TransactionTransfert transfert)
        {
            var compteSource = await _compteRepo.GetByIdAsync(transfert.CompteId);
            var compteDest = await _compteRepo.GetByNumeroAsync(transfert.NumeroCompteDestinataire);

            if (compteSource == null || compteDest == null)
                return false;
            if (compteSource.Solde < transfert.Montant)
                return false;

            compteSource.Solde -= transfert.Montant;
            compteDest.Solde += transfert.Montant;

            await _compteRepo.UpdateAsync(compteSource);
            await _compteRepo.UpdateAsync(compteDest);
            await _repo.AddAsync(transfert);
            return true;
        }

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<Dictionary<string, int>> GetStatsParTypeAsync()
        {
            var transactions = await _repo.GetAllWithDetailsAsync();
            return transactions
                .GroupBy(t => t.Type)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public async Task<Dictionary<string, double>> GetMontantParDABAsync()
        {
            var transactions = await _repo.GetAllWithDetailsAsync();
            return transactions
                .GroupBy(t => t.DAB.DABId)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Montant));
        }
    }
}