using DTOs.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class CompOfferAnonymAddDTO : IDTO
    {
        public int ToWhom { get; set; }
        public int ApplicationType { get; set; }
        public string Message { get; set; }
        public IFormFile? File { get; set; }

        public CompOfferAnonymAddDTO() { }

        public CompOfferAnonymAddDTO(int toWhom, int applicationType, string message)
        {
            ToWhom = toWhom;
            ApplicationType = applicationType;
            Message = message;
        }
    }
}
