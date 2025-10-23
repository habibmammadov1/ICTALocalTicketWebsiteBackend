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
    public class TeamMembersUpdateDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public IFormFile Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedIn { get; set; }

        public TeamMembersUpdateDTO()
        {
        }

        public TeamMembersUpdateDTO(int id, string name, string surname, string position, IFormFile photo, string email, string phone, string linkedIn)
        {
            Id = id;
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
