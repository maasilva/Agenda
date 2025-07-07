using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Linq;
using System.Security.Claims;

namespace Agenda.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly AgendaContext _context;
        public ChangePasswordModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SenhaAtual { get; set; }
        [BindProperty]
        public string NovaSenha { get; set; }
        [BindProperty]
        public string ConfirmarSenha { get; set; }
        public string Erro { get; set; }
        public string Sucesso { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(SenhaAtual) || string.IsNullOrEmpty(NovaSenha) || string.IsNullOrEmpty(ConfirmarSenha))
            {
                Erro = "Todos os campos são obrigatórios.";
                return Page();
            }
            if (NovaSenha != ConfirmarSenha)
            {
                Erro = "A nova senha e a confirmação não coincidem.";
                return Page();
            }
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == userId);
            if (usuario == null || usuario.SenhaHash != SeedData.HashSenha(SenhaAtual))
            {
                Erro = "Senha atual incorreta.";
                return Page();
            }
            usuario.SenhaHash = SeedData.HashSenha(NovaSenha);
            _context.SaveChanges();
            Sucesso = "Senha alterada com sucesso.";
            return Page();
        }
    }
}
