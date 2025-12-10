using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class ExchangeServiceProvider
    {
        public ExchangeService Connect()
        {
            var service = new ExchangeService(ExchangeVersion.Exchange2013_SP1)
            {
                Credentials = new WebCredentials("net.icta@icta.az", "HABi@jP9797!")
            };

            service.Url = new Uri("https://mail.icta.az/EWS/Exchange.asmx");

            return service;
        }
    }
}
