using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prueba_Tencica_Core.Interfaces;
using Prueba_Tencica_Core.Models;

namespace Prueba_Tecnica.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly string _secretKey = "Passw0rdLuis_Eduardo_Criollo_130491";  
        private readonly string _issuer = "pruebaTecnica";  
        private readonly string _audience = "Double V Partners";  

        public string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, usuario.UsuarioNombre),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),  
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

            
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true; 
            }
            catch (Exception)
            {
                return false; 
            }
        }

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            try
            {
                return await _usuarioRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los usuarios", ex);
            }
        }

        public async Task<Usuario> GetUsuariosByIdAsync(int id)
        {
            try
            {
                return await _usuarioRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el usuario con ID {id}", ex);
            }
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            try
            {
                await _usuarioRepository.Add(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario", ex);
            }
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            try
            {
                await _usuarioRepository.Update(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario", ex);
            }
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            try
            {
                await _usuarioRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el usuario con ID {id}", ex);
            }
        }
        public async Task<Usuario> LoginAsync(string usuarioNombre, string pass)
        {
            try
            {
                var usuario = await _usuarioRepository.LoginAsync(usuarioNombre, pass);
                if (usuario == null)
                {
                    return null;
                }
                return usuario; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar el login", ex);
            }
        }
    }
}
