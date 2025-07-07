using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly AgendaContext _context;
        public IndexModel(AgendaContext context)
        {
            _context = context;
        }
        public IList<Usuario> Usuarios { get; set; }
        public void OnGet()
        {
            Usuarios = _context.Usuarios.ToList();
        }
    }
}
