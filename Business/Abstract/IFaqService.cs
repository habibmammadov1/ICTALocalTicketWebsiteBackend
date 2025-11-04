using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFaqService
    {
        Task<List<Faq>> GetAllAsync();
        Task<Faq> GetByIdAsync(int id);
        Task AddAsync(Faq faq);
        Task UpdateAsync(Faq faq);
        Task DeleteAsync(int id);
    }
}


