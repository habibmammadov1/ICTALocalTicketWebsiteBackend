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
    [Table("COMP_OFFER")]
    public class CompOffer : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAME_SURNAME")]
        public string? NameSurname { get; set; }   // Full name of the applicant
        [Column("EMAIL")]
        public string? Email { get; set; }         // Optional email
        [Column("TO_WHOM")]
        public int ToWhom { get; set; }        // The person or department receiving the application
        [Column("APPLICATION_TYPE")]
        public int ApplicationType { get; set; } // Type/category of application
        [Column("MESSAGE")]
        public string Message { get; set; }       // Application content / message
        [Column("DATE_OF_APP")]
        public DateTime DateOfApp { get; set; } = DateTime.Now;       // Date of application
        [Column("FILE_PATH")]
        public string? FilePath { get; set; }      // File URL or server path to attachment

        public CompOffer() { }

        public CompOffer(int toWhom, int applicationType, string message, string? nameSurname = null, string? email = null, string? filePath = null)
        {
            ToWhom = toWhom;
            ApplicationType = applicationType;
            Message = message;
            NameSurname = nameSurname;
            Email = email;
            FilePath = filePath;
        }
    }
}