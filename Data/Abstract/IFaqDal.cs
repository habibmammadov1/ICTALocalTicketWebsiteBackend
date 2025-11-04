using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IFaqDal
    {
        Task<List<Faq>> GetAllAsync();
        Task<Faq> GetByIdAsync(int id);
        Task AddAsync(Faq faq);
        Task UpdateAsync(Faq faq);
        Task DeleteAsync(int id);
    }
}


