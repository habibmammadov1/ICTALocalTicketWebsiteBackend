using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Data.Abstract;
using Entities;

namespace Data.Concrete
{
    public class EfFaqDal : EfEntityRepositoryBase<Faq, AppDbContext>, IFaqDal
    {
    }
}

