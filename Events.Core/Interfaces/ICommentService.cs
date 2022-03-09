using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Shared.OperationResults;

namespace Events.Core.Interfaces
{
    public interface ICommentService
    {
        OperationResult<Comment> Get(Guid id);
        OperationResult<IReadOnlyCollection<Comment>> GetAll();
        OperationResult<Comment> Create(CommentDto dto);
        OperationResult Update(CommentDto category);
        OperationResult Delete(Guid id);
    }
}
