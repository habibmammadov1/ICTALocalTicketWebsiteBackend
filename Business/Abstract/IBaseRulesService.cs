using Core.Utilities.Results;
using DTOs.Concrete;
using Entities;

namespace Business.Abstract
{
    public interface IBaseRulesService
    {
        Task<IDataResult<List<BaseRules>>> GetAllBaseRulesAsync();
        Task<IDataResult<BaseRules>> GetBaseRuleByIdAsync(int id);
        Task<IDataResult<List<BaseRules>>> GetBaseRuleByBaseRuleIdAsync(int id);
        Task<IDataResult<BaseRulesAddResultDTO>> AddBaseRuleAsync(BaseRulesAddDTO baseRulesAddDTO, int userId);
        Task<IDataResult<BaseRulesUpdateDTO>> UpdateBaseRuleAsync(BaseRulesUpdateDTO baseRulesUpdateDTO, int userId);
        Task<IResult> DeleteBaseRuleAsync(int id);
        Task<IResult> ActiveDeactiveRuleAsync(int id, int activeDeactive); // 1 - activate, 2 - deactivate
    }
}
