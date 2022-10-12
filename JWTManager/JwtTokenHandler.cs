using JWTManager.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "asdasd$2323ASDasdasd3424sad2987fH";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private List<UserAccount> _userAccounts;
        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccount>
            {
             new UserAccount { UserName = "admin",Password="admin",Role="Admin" },
             new UserAccount { UserName = "user",Password="user",Role="User" },
            };
        }

        public AuthenticationResponse? GenerateToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            var userAccount = _userAccounts.Where(x => x.UserName == request.UserName && x.Password == request.Password).FirstOrDefault();
            if (userAccount is null)
                return null;

            var tokenExpiryTimestamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name,userAccount.UserName ),
                new Claim("Role", userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                );


            var descriptor = new SecurityTokenDescriptor
            {                
                Expires   = tokenExpiryTimestamp,
                SigningCredentials = signingCredentials,                
                Subject = claimIdentity
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(descriptor);

            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimestamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };

        }
}
}
