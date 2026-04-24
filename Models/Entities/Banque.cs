using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace GestionDAB.Models.Entities
{
    public class Banque
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le code est obligatoire")]
        [Display(Name = "Code")]
        public int Code { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Nom")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "La rue est obligatoire")]
        [StringLength(200)]
        [Display(Name = "Rue")]
        public string Rue { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ville est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Ville")]
        public string Ville { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Compte> Comptes { get; set; } = new List<Compte>();
    }
}