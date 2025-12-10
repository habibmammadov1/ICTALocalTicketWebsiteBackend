using DTOs.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class TeamMembersAddDTO : IDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public IFormFile Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedIn { get; set; }

        public TeamMembersAddDTO()
        {
        }

        public TeamMembersAddDTO(string name, string surname, string position, IFormFile photo, string email, string phone, string linkedIn)
        {
            Name = name;
            Surname = surname;
            Position = position;
            Photo = photo;
            Email = email;
            Phone = phone;
            LinkedIn = linkedIn;
        }
    }
}
