using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        public string SenhaHash { get; set; }
    }
}
