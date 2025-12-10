using Core.Utilities.Results;
using DTOs.Concrete.Novelty;
using Entities.Novelty;

namespace Business.Abstract
{
    public interface INoveltyService
    {
        Task<IDataResult<List<BaseNovelty>>> GetAllAsync();
        Task<IDataResult<BaseNovelty>> GetByIdAsync(int id);
        Task<IDataResult<List<BaseNovelty>>> GetByNoveltyIdAsync(int noveltyId);
        Task<IDataResult<NoveltyAddDTO>> AddAsync(NoveltyAddDTO noveltyAddDTO);
        Task<IResult> RemoveAsync(int id);
        Task<IDataResult<NoveltyUpdateDTO>> UpdateAsync(NoveltyUpdateDTO noveltyUpdateDTO);
        //--------------
        Task<IResult> ActiveDeactiveAsync(int id, int status);
        Task<IResult> LikeActiveDeactiveAsync(int id, int likeActiveDeactive, string whoLikes);
        Task<IResult> DislikeActiveDeactiveAsync(int id, int dislikeActiveDeactive, string whoLikes);



        /*
         
         View Count - via getbyid

         Active/Deactive
         Like Active/Deactive
         Dislike Active/Deactive
         */
    }
}
