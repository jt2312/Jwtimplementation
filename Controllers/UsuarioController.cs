using ApiNetCoreJwt6._0.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using System.Security.Claims;
using ApiNetCoreJwt6._0.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiNetCoreJwt6._0.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _config;
        public UsuarioController(IConfiguration config) {
            
            _config = config;
        }
        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            string user = data.usuario.ToString();
            string contraseña = data.contraseña.ToString();

            Usuario usuario = Usuario.DataBase().Where(x => x.usuario == user && x.contraseña == contraseña).FirstOrDefault();
            if (usuario == null)
            {
                return new
                {
                    succes = false,
                    message = "Credenciales Incorrectas",
                    result = ""
                };

            }
            var jwt = _config.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim ("Id", usuario.Id.ToString()),
                new Claim ("usuario", usuario.usuario)
            };
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signIn
                );
            return new
            {
                succes = true,
                message = "exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        
        }
    }
}
