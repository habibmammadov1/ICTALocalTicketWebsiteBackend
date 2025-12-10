using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class LdapUserDto
    {
        public string Username { get; set; }
        public string? UserPrincipalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? Office { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }


        public LdapUserDto() { }

        public LdapUserDto(string username, string? userPrincipalName, string firstName, string lastName, string? displayName, string? description, string? office, string? phone, string? email, string? department, string? title)
        {
            Username = username;
            UserPrincipalName = userPrincipalName;
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Description = description;
            Office = office;
            Phone = phone;
            Email = email;
            Department = department;
            Title = title;
        }
    }
}
