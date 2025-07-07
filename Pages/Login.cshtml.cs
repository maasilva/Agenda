using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace Agenda.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AgendaContext _context;
        public LoginModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Login { get; set; }
        [BindProperty]
        public string Senha { get; set; }
        public string Erro { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Senha))
            {
                Erro = "Login e senha obrigatórios.";
                return Page();
            }
            var senhaHash = SeedData.HashSenha(Senha);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Login == Login && u.SenhaHash == senhaHash);
            if (usuario == null)
            {
                Erro = "Usuário ou senha inválidos.";
                return Page();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToPage("/Compromissos/Index");
        }
    }
}
