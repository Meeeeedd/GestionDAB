using System.ComponentModel.DataAnnotations;

namespace GestionDAB.Models.Entities
{
    public class DAB
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'identifiant DAB est obligatoire")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Identifiant DAB")]
        public string DABId { get; set; } = string.Empty;

        [Required(ErrorMessage = "La localisation est obligatoire")]
        [StringLength(200)]
        [Display(Name = "Localisation")]
        public string Localisation { get; set; } = string.Empty;

        [Display(Name = "En service")]
        public bool EstEnService { get; set; } = true;

        // Navigation property
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}