using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Exceptions;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.OperationResults;
using Events.Core.Specifications.CategorySpecifications;
using Events.Core.Specifications.EventsSpecifications;
using Microsoft.Extensions.Logging;

namespace Events.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Office> _officeRepository;
        private readonly ILogger<EventService> _logger;

        public EventService(
            IRepository<Event> eventsRepository,
            IRepository<Category> categoriesRepository,
            IRepository<Office> officeRepository,
            ILogger<EventService> logger)
        {
            _eventsRepository = eventsRepository.ThrowIfNull(nameof(eventsRepository));
            _categoriesRepository = categoriesRepository.ThrowIfNull(nameof(categoriesRepository));
            _officeRepository = officeRepository.ThrowIfNull(nameof(officeRepository));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        public OperationResult<Event> Create(EventDto dto)
        {
            try
            {
                var categories = _categoriesRepository.List(new CategoriesByIdSpecification(dto.CategoryIds));
                var office = _officeRepository.GetById(dto.OfficeId);
                if (office is null) throw new NotFoundException($"No office Found for Id: {dto.OfficeId}");

                var @event = new Event(dto.Name, dto.Discription, dto.Duration, office, dto.Location, categories.ToList(), dto.Attendees, null, dto.Status);
                _eventsRepository.Add(@event);

                return OperationResult<Event>.Succeeded(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Event>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                var @event = Get(id).Payload;
                if (@event is null) throw new NotFoundException($"No category found with id: {id}");

                _eventsRepository.Delete(@event);

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }
        }

        public OperationResult<Event> Get(Guid id)
        {
            try
            {
                var events = _eventsRepository.List(new GetEventByIdIncludeAllSpecification(id));
                var @event = events.SingleOrDefault();
                if (@event is null) throw new NotFoundException($"No category found with id: {id}");

                return OperationResult<Event>.Succeeded(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Event>.Failed(ex, ex.Message);
            }
        }

        public OperationResult<IReadOnlyCollection<Event>> GetAll()
        {
            try
            {
                var @event = _eventsRepository.List();

                return OperationResult<IReadOnlyCollection<Event>>.Succeeded(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<IReadOnlyCollection<Event>>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Update(EventDto dto)
        {
            try
            {
                var eventResult = Get(dto.Id);
                if (!eventResult.Success) throw eventResult.Exception!;
                var dbEvent = eventResult.Payload!;

                var office = _officeRepository.GetById(dto.OfficeId);
                if (office is null) throw new NotFoundException($"No Office Found with Id: {dto.OfficeId}");

                var categories = _categoriesRepository.List(new CategoriesByIdSpecification(dto.CategoryIds));
                if (categories.Count != dto.CategoryIds.Count) throw new NotFoundException($"Could not map all CategoryIds to Categories.");

                dbEvent.SetName(dto.Name);
                dbEvent.SetDiscription(dto.Discription);
                dbEvent.SetOffice(office);
                dbEvent.SetLocation(dto.Location);
                dbEvent.SetDuration(dto.Duration);
                dbEvent.SetCategories(categories.ToList());
                dbEvent.SetAttendees(dto.Attendees);
                dbEvent.SetStatus(dto.Status);

                _eventsRepository.Edit(dbEvent);

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }
        }
    }
}
