using System.ComponentModel.DataAnnotations;

namespace GestionDAB.Models.Entities
{
    public class Compte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le numéro de compte est obligatoire")]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Numéro de compte")]
        public string NumeroCompte { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le propriétaire est obligatoire")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Propriétaire")]
        public string Proprietaire { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le solde ne peut pas être négatif")]
        [Display(Name = "Solde")]
        public double Solde { get; set; }

        [Required]
        [Display(Name = "Type de compte")]
        public TypeCompte Type { get; set; }

        // Foreign key
        [Display(Name = "Banque")]
        public int BanqueId { get; set; }

        // Navigation properties
        public Banque Banque { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}