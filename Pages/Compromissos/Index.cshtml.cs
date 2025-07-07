using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenda.Data;
using Agenda.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Pages.Compromissos
{
    public class IndexModel : PageModel
    {
        private readonly AgendaContext _context;
        public IndexModel(AgendaContext context)
        {
            _context = context;
        }

        public IList<Compromisso> Compromissos { get; set; }

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                Compromissos = new List<Compromisso>();
                return;
            }
            var userId = int.Parse(userIdClaim.Value);
            Compromissos = await _context.Compromissos
                .Where(c => c.UsuarioId == userId)
                .OrderBy(c => c.DataHora)
                .ToListAsync();
        }
    }
}
