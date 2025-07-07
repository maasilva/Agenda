using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Linq;

namespace Agenda.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AgendaContext _context;
        public RegisterModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Nome { get; set; }
        [BindProperty]
        public string Login { get; set; }
        [BindProperty]
        public string Senha { get; set; }
        public string Erro { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Senha))
            {
                Erro = "Todos os campos são obrigatórios.";
                return Page();
            }
            if (_context.Usuarios.Any(u => u.Login == Login))
            {
                Erro = "Login já existe.";
                return Page();
            }
            var usuario = new Usuario
            {
                Nome = Nome,
                Login = Login,
                SenhaHash = SeedData.HashSenha(Senha)
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToPage("/Login");
        }
    }
}
