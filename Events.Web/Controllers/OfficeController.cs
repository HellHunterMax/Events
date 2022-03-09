using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Events.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpGet, Authorize(Roles = "Manager")]
        public ActionResult<IReadOnlyCollection<Office>> Get()
        {
            var result = _officeService.GetAll();

            if (!result.Success) return NotFound();

            return Ok(result.Payload);
        }

        [HttpGet("{id}")]
        public ActionResult<Office> Get(Guid id)
        {
            var result = _officeService.Get(id);

            if (!result.Success) return NotFound(result.Exception!.Message);

            return Ok(result.Payload);
        }

        [HttpPost]
        public ActionResult<Office> Post(OfficeDto dto)
        {
            var result = _officeService.Create(dto);

            if (!result.Success) return BadRequest(result.Message);

            return result.Payload!;
        }

        [HttpPut]
        public ActionResult Put(OfficeDto dto)
        {
            var result = _officeService.Update(dto);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult JsonPatchWithModelState(Guid id, [FromBody] JsonPatchDocument<OfficeDto> patchDoc)
        {
            var result = _officeService.Get(id);

            if (!result.Success) return BadRequest(result.Message);

            var office = result.Payload!;

            OfficeDto officeDto = new OfficeDto
            {
                Id = office.Id,
                Name = office.Name,
                Discription = office.Discription,
                Location = office.Location,
                Email = office.Email,
                PhoneNumber = office.PhoneNumber,
                Status = office.Status,
            };

            patchDoc.ApplyTo(officeDto);

            var updateResult = _officeService.Update(officeDto);

            if (!updateResult.Success) return BadRequest(updateResult.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _officeService.Delete(id);

            if (!result.Success) return NotFound(result.Message);

            return NoContent();
        }
    }
}
