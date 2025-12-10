using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("TeamMembers")]
    public class TeamMember : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAME")]
        [MaxLength(18)]
        public string Name { get; set; }
        [Column("SURNAME")]
        [MaxLength(25)]
        public string Surname { get; set; }
        [Column("POSITION")]
        [MaxLength(50)]
        public string Position { get; set; }
        [Column("PHOTO_PATH")]
        [MaxLength(500)]
        public string PhotoPath { get; set; }
        [Column("EMAIL")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Column("PHONE")]
        [MaxLength(25)]
        public string Phone { get; set; }
        [Column("LINKEDIN")]
        [MaxLength(500)]
        public string LinkedIn { get; set; }
        [Column("STATUS")]
        public int Status { get; set; } = 1;

        public TeamMember()
        {
            
        }

        public TeamMember(string name, string surname, string position, string photoPath, string email, string phone, string linkedIn, int status)
        {
            Name = name;
            Surname = surname;
            Position = position;
            PhotoPath = photoPath;
            Email = email;
            Phone = phone;
            LinkedIn = linkedIn;
            Status = status;
        }

        public TeamMember(int id, string name, string surname, string position, string photoPath, string email, string phone, string linkedIn, int status)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Position = position;
            PhotoPath = photoPath;
            Email = email;
            Phone = phone;
            LinkedIn = linkedIn;
            Status = status;
        }
    }
}
