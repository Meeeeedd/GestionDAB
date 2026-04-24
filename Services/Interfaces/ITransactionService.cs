using GestionDAB.Models.Entities;

namespace GestionDAB.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetByCompteAsync(int compteId);
        Task<IEnumerable<Transaction>> SearchAsync(string? libelle, DateTime? from, DateTime? to, string? type, double? montantMin, double? montantMax);
        Task<bool> EffectuerRetraitAsync(TransactionRetrait retrait);
        Task<bool> EffectuerTransfertAsync(TransactionTransfert transfert);
        Task DeleteAsync(int id);
        Task<Dictionary<string, int>> GetStatsParTypeAsync();
        Task<Dictionary<string, double>> GetMontantParDABAsync();
    }
}