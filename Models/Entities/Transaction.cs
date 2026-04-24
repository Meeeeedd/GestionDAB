using System.ComponentModel.DataAnnotations;

namespace GestionDAB.Models.Entities
{
    public abstract class Transaction
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être supérieur à 0")]
        [Display(Name = "Montant (DT)")]
        public double Montant { get; set; }

        [StringLength(200)]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; } = string.Empty;

        // Discriminator handled by EF Core (TPH)
        public string Type { get; set; } = string.Empty;

        // Foreign keys
        [Display(Name = "Compte")]
        public int CompteId { get; set; }

        [Display(Name = "DAB")]
        public int DABId { get; set; }

        // Navigation properties
        public Compte Compte { get; set; } = null!;
        public DAB DAB { get; set; } = null!;
    }
}