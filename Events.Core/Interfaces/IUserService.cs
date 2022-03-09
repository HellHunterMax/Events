using Events.Core.Dtos;
using Events.Core.Shared.OperationResults;

namespace Events.Core.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<UserReplyDto>> Get(string id);
        OperationResult<IReadOnlyCollection<UserReplyDto>> GetAll();
        Task<OperationResult> Update(UserUpdateDto dto);
        Task<OperationResult> Delete(string id);
    }
}
