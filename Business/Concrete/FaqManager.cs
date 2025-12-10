using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Business.Abstract;
using Data.Abstract;
using Entities;

namespace Business.Concrete
{
    public class FaqManager : IFaqService
    {
        private readonly IFaqDal _faqDal;

        public FaqManager(IFaqDal faqDal)
        {
            _faqDal = faqDal;
        }

        public async Task<List<Faq>> GetAllAsync()
        {
            return await _faqDal.GetAllAsync();
        }

        public async Task<Faq> GetByIdAsync(int id)
        {
            return await _faqDal.GetAsync(faq => faq.Id == id);
        }

        public async Task AddAsync(Faq faq)
        {
            await _faqDal.AddAsync(faq);
        }

        public async Task DeleteAsync(int id)
        {
            Faq faqToDelete = await _faqDal.GetAsync(faq => faq.Id == id);
            await _faqDal.RemoveAsync(faqToDelete);
        }

        public async Task UpdateAsync(Faq faq)
        {
            await _faqDal.UpdateAsync(faq);
        }
    }
}
