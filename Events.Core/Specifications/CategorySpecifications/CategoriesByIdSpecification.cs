using Events.Core.Entities;

namespace Events.Core.Specifications.CategorySpecifications
{
    public class CategoriesByIdSpecification : BaseSpecification<Category>
    {
        public CategoriesByIdSpecification(IEnumerable<Guid> ids) : base(c => ids.Contains(c.Id))
        {
        }
        public CategoriesByIdSpecification(Guid id) : base(c => c.Id == id) { }
    }
}
