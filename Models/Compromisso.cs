using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Compromisso
    {
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Local { get; set; }

        [Required]
        [Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }

        // Relacionamento com o usuário
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
