using Events.Core.Entities;

namespace Events.Core.Specifications.CommentSpecifications
{
    public class GetCommentByIdAndIncludeSpecification : BaseSpecification<Comment>
    {
        public GetCommentByIdAndIncludeSpecification(Guid id) : base(x => x.Id == id)
        {
            AddInclude(c => c.Event);
        }
    }
}
