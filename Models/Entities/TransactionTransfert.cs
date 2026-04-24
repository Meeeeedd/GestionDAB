using System.ComponentModel.DataAnnotations;

namespace GestionDAB.Models.Entities
{
    public class TransactionTransfert : Transaction
    {
        [Required(ErrorMessage = "Le numéro de compte destinataire est obligatoire")]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Numéro de compte destinataire")]
        public string NumeroCompteDestinataire { get; set; } = string.Empty;
    }
}