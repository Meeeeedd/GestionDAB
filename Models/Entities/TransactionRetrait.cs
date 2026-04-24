using System.ComponentModel.DataAnnotations;

namespace GestionDAB.Models.Entities
{
    public class TransactionRetrait : Transaction
    {
        [Display(Name = "Autre agence")]
        public bool AutreAgence { get; set; }
    }
}