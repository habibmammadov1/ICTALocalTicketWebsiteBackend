using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class AuthLoginDTO : IDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public AuthLoginDTO()
        {
            
        }

        public AuthLoginDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
