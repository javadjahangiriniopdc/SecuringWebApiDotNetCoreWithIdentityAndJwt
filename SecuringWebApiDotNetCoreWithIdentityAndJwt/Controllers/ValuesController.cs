using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecuringWebApiDotNetCoreWithIdentityAndJwt.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecuringWebApiDotNetCoreWithIdentityAndJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        [HttpGet("getFruits")]
        [AllowAnonymous]
        public ActionResult GetFruits()
        {
            List<string> mylist = new List<string>() { "apples", "bannanas" };
            return Ok(mylist);
        }

        [HttpGet("getFruitsAuthenticated")]
        public ActionResult GetFruitsAuthenticated()
        {
            List<string> mylist = new List<string>() { "organic apples", "organic bannanas" };
            return Ok(mylist);
        }
        [AllowAnonymous]
        [HttpPost("getToken")]
        public async Task<ActionResult> GetToken([FromBody] MyLoginModelType myLoginModelType)
        {
            if (myLoginModelType.Email == "javad.jahangiri.niopdc@gmail.com" && myLoginModelType.Password=="Pa$sw0rd")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key= Encoding.ASCII.GetBytes("@#MY_BIG_SECRET_KEY@#");
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, myLoginModelType.Email)
                    }
                   ),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);
            return  Ok(new { token = tokenString } );
            }
            else
            {
                return Unauthorized("try again");
            }

        }
    }
}
