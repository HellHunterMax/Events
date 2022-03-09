using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Shared.OperationResults;

namespace Events.Core.Interfaces
{
    public interface IEventService
    {
        OperationResult<Event> Get(Guid id);
        OperationResult<IReadOnlyCollection<Event>> GetAll();
        OperationResult<Event> Create(EventDto dto);
        OperationResult Update(EventDto category);
        OperationResult Delete(Guid id);
    }
}
