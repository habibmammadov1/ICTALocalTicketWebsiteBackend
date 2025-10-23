using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FooterManager : IFooterService
    {
        private readonly IFooterDal _footerDal;

        public FooterManager(IFooterDal footerDal)
        {
            _footerDal = footerDal;
        }

        public IDataResult<Footer> FooterUpdate(Footer footer)
        {
            Footer footerCheck = _footerDal.Get(f => true);

            footer.SetDateYear(DateTime.Now.Year);
            footer.SetLastUpdated(DateTime.Now);

            if (footerCheck == null)
            {
                _footerDal.Add(footer); 
            }

            else
            {
                footer.SetId(footerCheck.id);
                _footerDal.Update(footer);
            }            
            
            return new SuccessDataResult<Footer>(footer);
        }

        public IDataResult<Footer> getFooter()
        {
            return new SuccessDataResult<Footer>(_footerDal.GetLastModifiedFooter());
        }
    }
}
