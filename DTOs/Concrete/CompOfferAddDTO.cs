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
    public class CompOfferAddDTO : IDTO
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public int ToWhom { get; set; }
        public int ApplicationType { get; set; }
        public string Message { get; set; }
        public IFormFile? File { get; set; }

        public CompOfferAddDTO() { }

        public CompOfferAddDTO(int toWhom, int applicationType, string message, string nameSurname, string email)
        {
            ToWhom = toWhom;
            ApplicationType = applicationType;
            Message = message;
            NameSurname = nameSurname;
            Email = email;
        }
    }
}
