using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenda.Data;
using Agenda.Models;
using System.Threading.Tasks;

namespace Agenda.Pages.Compromissos
{
    public class EditModel : PageModel
    {
        private readonly AgendaContext _context;
        public EditModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Compromisso Compromisso { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return RedirectToPage("/Login");
            var userId = int.Parse(userIdClaim.Value);
            Compromisso = await _context.Compromissos.FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == userId);
            if (Compromisso == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return RedirectToPage("/Login");
            var userId = int.Parse(userIdClaim.Value);
            var compromissoDb = await _context.Compromissos.FirstOrDefaultAsync(c => c.Id == Compromisso.Id && c.UsuarioId == userId);
            if (compromissoDb == null)
                return NotFound();
            compromissoDb.Descricao = Compromisso.Descricao;
            compromissoDb.Local = Compromisso.Local;
            compromissoDb.DataHora = Compromisso.DataHora;
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
