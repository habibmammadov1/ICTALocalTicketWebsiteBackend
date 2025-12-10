using Core.DataAccess.EntityFramework;
using Data.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class EfTeamMembersDal : EfEntityRepositoryBase<TeamMember, AppDbContext>, ITeamMemberDal
    {
    }
}
