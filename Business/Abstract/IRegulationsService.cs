using Core.Utilities.Results;
using DTOs.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRegulationsService
    {
        Task<IDataResult<RegulationsAddDTO>> updateRegulationsAsync(RegulationsAddDTO regulationsAddDTO); // add or update
        Task<IDataResult<Regulations>> getRegulationsAsync(int id, string rootPath);
    }
}
