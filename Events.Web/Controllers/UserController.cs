using Events.Core.Dtos;
using Events.Core.Interfaces;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Events.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService.ThrowIfNull(nameof(userService));
        }

        [HttpGet("Get All")]
        public ActionResult<UserReplyDto> GetAll()
        {
            var result = _userService.GetAll();

            if (!result.Success) return BadRequest();

            return Ok(result.Payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReplyDto>> Get(string id)
        {
            var result = await _userService.Get(id);

            if (!result.Success) return BadRequest();

            return Ok(result.Payload);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return BadRequest($"Id cant ben null or empty.");

            var result = await _userService.Delete(id);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UserUpdateDto dto)
        {
            var result = await _userService.Update(dto);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(string id, [FromBody] JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            var result = await _userService.Get(id);

            if (!result.Success) return BadRequest(result.Message);

            var user = result.Payload!;

            UserUpdateDto userDto = new UserUpdateDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            patchDoc.ApplyTo(userDto);

            var updateResult = await _userService.Update(userDto);

            if (!updateResult.Success) return BadRequest(updateResult.Message);

            return NoContent();
        }

    }
}
