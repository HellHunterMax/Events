using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Shared.OperationResults;

namespace Events.Core.Interfaces
{
    public interface ICategoryService
    {
        OperationResult<Category> Get(Guid id);
        OperationResult<IReadOnlyCollection<Category>> GetAll();
        OperationResult<Category> Create(string name);
        OperationResult Update(CategoryDto category);
        OperationResult Delete(Guid id);
    }
}
