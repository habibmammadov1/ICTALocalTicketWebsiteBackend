using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    [Table("AUTH")]
    public class Auth : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [MaxLength(20)]
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [MaxLength(20)]
        [Column("F_NAME")]
        public string FName { get; set; }

        [MaxLength(20)]
        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        [MaxLength(40)]
        [Column("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }

        [MaxLength(80)]
        [Column("MAIL_TOKEN")]
        public string? MailToken { get; set; }

        [MaxLength(30)]
        [Column("USERNAME")]
        public string Username { get; set; }

        [MaxLength(50)]
        [Column("EMAIL")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("STATUS")]
        public int Status { get; set; } = 0; // 0 - inactive, 1 - active

        public Auth() { }

        public Auth(
            int id,
            string firstName,
            string lastName,
            string fName,
            string phoneNumber,
            string departmentName,
            string username,
            string email,
            string password,
            int status)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FName = fName;
            PhoneNumber = PhoneNumber;
            DepartmentName = departmentName;
            Username = username;
            Email = email;
            Password = password;
            Status = status;
        }
    }
}
