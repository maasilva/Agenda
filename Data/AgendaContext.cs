using Microsoft.EntityFrameworkCore;
using Agenda.Models;

namespace Agenda.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Compromisso> Compromissos { get; set; }
        public DbSet<Agenda.Models.Usuario> Usuarios { get; set; }
    }
}
