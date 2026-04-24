using System.ComponentModel.DataAnnotations;

namespace GestionDAB.Models.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Entite { get; set; } = string.Empty;

        [StringLength(450)]
        public string UserId { get; set; } = string.Empty;

        [StringLength(100)]
        public string UserEmail { get; set; } = string.Empty;

        public DateTime DateAction { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Details { get; set; } = string.Empty;
    }
}