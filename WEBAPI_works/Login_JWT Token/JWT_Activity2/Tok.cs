using JWT_Activity2.Interface;
using JWT_Activity2.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT_Activity2
{
    public class Tok:ITokenInterface
    {
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpAccessor;

        public Tok(IConfiguration config, IHttpContextAccessor httpAccessor)
        {
            _config = config;
            _httpAccessor = httpAccessor;
        }

        public string? CreateToken(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user),"user object can not be null");
            }

            string SecretKey=_config.GetSection("Jwt:Key").Value;
            if(string.IsNullOrEmpty(SecretKey))
            {
                throw new InvalidOperationException("Secret Key is missing or empty in configuaration");   
            }


            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.role.ToString())

            };

            
            var  Key=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                  claims: claims,
                  signingCredentials: creds,
                  expires: DateTime.Now.AddMinutes(30)
                );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }

        public string  GetuserName()
        {
            string name=string.Empty;
            if(_httpAccessor.HttpContext!=null)
            {
                name = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return name;
        }

    }
}
