using Domain.Entities;
using Infrastructure.Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Identity.Services
{
    public class GeracaoUsuariosPerfisIniciais : IGeracaoUsuariosPerfisIniciais
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Perfil> _roleManager;

        public GeracaoUsuariosPerfisIniciais(RoleManager<Perfil> roleManager,
              UserManager<Usuario> userManager) {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void GerarPerfis() {
            string[] perfis = { "atendente", "cliente", "estagiario", "professor" };

            foreach (var perfilNome in perfis) {
                if (!_roleManager.RoleExistsAsync(perfilNome).Result) {
                    Perfil perfil = new();
                    perfil.Name = perfilNome;
                    perfil.NormalizedName = perfil.Name.ToUpper();
                    IdentityResult roleResult = _roleManager.CreateAsync(perfil).Result;
                }
            }
        }

        public void GerarUsuarios() {
            string[] perfis = { "atendente", "cliente", "estagiario", "professor" };

            foreach (var perfilNome in perfis) {
                string email = $"{perfilNome}@user.com.br";
                if (_userManager.FindByNameAsync(email).Result == null) {
                    Usuario usuario = new();
                    usuario.Name = perfilNome.First().ToString().ToUpper() + perfilNome.Substring(1);
                    usuario.UserName = email;
                    usuario.NormalizedUserName = usuario.UserName.ToUpper();
                    usuario.Email = usuario.UserName;
                    usuario.NormalizedEmail = usuario.Email.ToUpper();
                    usuario.LockoutEnabled = false;
                    usuario.SecurityStamp = Guid.NewGuid().ToString();
                    usuario.PhoneNumber = "38123456789";

                    IdentityResult result = _userManager.CreateAsync(usuario, "Teste1@").Result;

                    if (result.Succeeded) {
                        _userManager.AddToRoleAsync(usuario, perfilNome).Wait();
                    }
                }
            }
        }

    }
}
