using Core.DataAccess.EntityFramework;
using Data.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class EfFooterDal : EfEntityRepositoryBase<Footer, AppDbContext>, IFooterDal
    {
        public Footer GetLastModifiedFooter()
        {
            using (var context = new AppDbContext())
            {
                var footer = context.Footers
                                    .OrderByDescending(f => f.lastUpdated)
                                    .FirstOrDefault();

                if (footer == null)
                {
                    // return a default object instead of null
                    return new Footer
                    (           
                        0,
                        DateTime.Now.Year,
                        "N/A",
                        "N/A",
                        "N/A",
                        DateTime.Now
                    );
                }

                return footer;
            }
        }
    }
}
