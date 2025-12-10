using DTOs.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILdapConnection1
    {
        public Task<List<LdapUserDto>> GetAllUsersAsync();
        public Task<bool> AuthenticateUser(string username, string password);
    }
}
