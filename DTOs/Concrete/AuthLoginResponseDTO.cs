using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class AuthLoginResponseDTO : IDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public AuthLoginResponseDTO()
        {
            
        }

        public AuthLoginResponseDTO(string username, string password, string token)
        {
            Username = username;
            Password = password;
            Token = token;
        }
    }
}
