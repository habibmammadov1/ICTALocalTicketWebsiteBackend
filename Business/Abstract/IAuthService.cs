using Core.Entities.Concrete;
using Core.Utilities.Results;
using DTOs.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<AuthRegisterDTO> registerAuth(AuthRegisterDTO authRegisterDTO);
        IDataResult<AuthLoginDTO> loginAuth(AuthLoginDTO authLoginDTO);
        IDataResult<AuthLoginResponseDTO> loginAuth2(AuthLoginDTO authLoginDTO);
        IResult verifyEmail(string token);
    }
}
