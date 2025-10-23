using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Data.Abstract;
using DTOs.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class EfAuthDal : EfEntityRepositoryBase<Auth, AppDbContext>, IAuthDal
    {
       
    }
}
