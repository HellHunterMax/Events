using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Shared.OperationResults;

namespace Events.Core.Interfaces
{
    public interface IOfficeService
    {
        OperationResult<Office> Get(Guid id);
        OperationResult<IReadOnlyCollection<Office>> GetAll();
        OperationResult<Office> Create(OfficeDto dto);
        OperationResult Update(OfficeDto category);
        OperationResult Delete(Guid id);
    }
}
