using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class AuthRegisterDTO : IDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FName { get; set; }

        public string PhoneNumber { get; set; }

        public string DepartmentName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AuthRegisterDTO()
        {
            
        }

        public AuthRegisterDTO(string firstName, string lastName, string fName, string phoneNumber, string departmentName, string username, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            FName = fName;
            PhoneNumber = phoneNumber;
            DepartmentName = departmentName;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
