using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Threading.Tasks;

namespace Agenda.Pages.Compromissos
{
    public class CreateModel : PageModel
    {
        private readonly AgendaContext _context;
        public CreateModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Compromisso Compromisso { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Login");
            }
            Compromisso.UsuarioId = int.Parse(userIdClaim.Value);
            _context.Compromissos.Add(Compromisso);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
