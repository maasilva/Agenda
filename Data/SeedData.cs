using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Agenda.Models;
using System.Security.Cryptography;
using System.Text;

namespace Agenda.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AgendaContext(
                serviceProvider.GetRequiredService<DbContextOptions<AgendaContext>>()))
            {
                if (context.Usuarios.Any())
                {
                    return; // DB já possui dados
                }
                // Cria usuário Admin padrão
                var admin = new Usuario
                {
                    Nome = "Administrador",
                    Login = "Admin",
                    SenhaHash = HashSenha("admin")
                };
                context.Usuarios.Add(admin);
                context.SaveChanges();
            }
        }

        public static string HashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }
}
