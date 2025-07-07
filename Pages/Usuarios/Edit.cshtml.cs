using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Linq;

namespace Agenda.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly AgendaContext _context;
        public EditModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string NovaSenha { get; set; }
        [BindProperty]
        public string ConfirmarSenha { get; set; }
        public string Erro { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(NovaSenha) || string.IsNullOrEmpty(ConfirmarSenha))
            {
                Erro = "Todos os campos são obrigatórios.";
                return Page();
            }
            if (NovaSenha != ConfirmarSenha)
            {
                Erro = "A nova senha e a confirmação não coincidem.";
                return Page();
            }
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == Id);
            if (usuario == null)
            {
                Erro = "Usuário não encontrado.";
                return Page();
            }
            usuario.SenhaHash = SeedData.HashSenha(NovaSenha);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
