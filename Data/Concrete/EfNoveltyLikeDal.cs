using Core.DataAccess.EntityFramework;
using Data.Abstract;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class EfNoveltyLikeDal : EfEntityRepositoryBase<NoveltyLike, AppDbContext>, INoveltyLikeDal
    {
    }
}
