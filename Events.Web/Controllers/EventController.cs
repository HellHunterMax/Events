using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Events.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IEmailService _emailService;

        public EventController(IEventService eventService, IEmailService emailService)
        {
            _eventService = eventService.ThrowIfNull(nameof(eventService));
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Event>> Get()
        {
            var result = _eventService.GetAll();

            if (!result.Success) return NotFound();

            return Ok(result.Payload);
        }

        [HttpGet("{id}")]
        public ActionResult<Event> Get(Guid id)
        {
            var result = _eventService.Get(id);

            if (!result.Success) return NotFound(result.Exception!.Message);

            return Ok(result.Payload);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Post(EventDto dto)
        {
            var result = _eventService.Create(dto);

            if (!result.Success) return BadRequest(result.Message);

            await _emailService.SendEventCreatedMailAsync(result.Payload!);

            return result.Payload!;
        }

        [HttpPut]
        public ActionResult Put(EventDto dto)
        {
            var result = _eventService.Update(dto);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult JsonPatchWithModelState(Guid id, [FromBody] JsonPatchDocument<EventDto> patchDoc)
        {
            var result = _eventService.Get(id);

            if (!result.Success) return BadRequest(result.Message);

            var @event = result.Payload!;

            var categoriesDto = new List<Guid>();

            foreach (var item in @event.Categories)
            {
                categoriesDto.Add(item.Id);
            }

            var attendees = new List<Email>();

            foreach (var item in @event.Attendees)
            {
                attendees.Add(new Email(item.Address));
            }

            EventDto eventDto = new EventDto()
            {
                Id = @event.Id,
                Name = @event.Name,
                Discription = @event.Discription,
                OfficeId = @event.Office.Id,
                Location = @event.Location,
                Duration = @event.Duration,
                CategoryIds = categoriesDto,
                Attendees = attendees,
                Status = @event.Status
            };

            patchDoc.ApplyTo(eventDto);

            var updateResult = _eventService.Update(eventDto);

            if (!updateResult.Success) return BadRequest(updateResult.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _eventService.Delete(id);

            if (!result.Success) return NotFound(result.Message);

            return NoContent();
        }
    }
}
