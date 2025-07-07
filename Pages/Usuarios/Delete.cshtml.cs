using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agenda.Data;
using Agenda.Models;
using System.Linq;

namespace Agenda.Pages.Usuarios
{
    public class DeleteModel : PageModel
    {
        private readonly AgendaContext _context;
        public DeleteModel(AgendaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Id { get; set; }
        public Usuario? Usuario { get; set; }
        public string Erro { get; set; }

        public void OnGet(int id)
        {
            Id = id;
            Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public IActionResult OnPost()
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == Id);
            if (usuario == null)
            {
                Erro = "Usuário não encontrado.";
                return Page();
            }
            if (usuario.Login == "Admin")
            {
                Erro = "Não é permitido excluir o usuário Admin.";
                Usuario = usuario;
                return Page();
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
