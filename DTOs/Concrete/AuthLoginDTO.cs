using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class AuthLoginDTO : IDTO
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
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
