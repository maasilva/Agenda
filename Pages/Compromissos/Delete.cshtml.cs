using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agenda.Data;
using Agenda.Models;
using System.Threading.Tasks;

namespace Agenda.Pages.Compromissos
{
    public class DeleteModel : PageModel
    {
        private readonly AgendaContext _context;
        public DeleteModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Compromisso? Compromisso { get; set; }

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
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return RedirectToPage("/Login");
            var userId = int.Parse(userIdClaim.Value);
            var compromissoId = Compromisso?.Id ?? 0;
            var compromisso = await _context.Compromissos.FirstOrDefaultAsync(c => c.Id == compromissoId && c.UsuarioId == userId);
            if (compromisso != null)
            {
                _context.Compromissos.Remove(compromisso);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
