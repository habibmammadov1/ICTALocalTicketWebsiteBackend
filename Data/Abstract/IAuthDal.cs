using Core.DataAccess;
using Core.Entities.Concrete;
using DTOs.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IAuthDal : IEntityRepository<Auth>
    {
        
    }
}
