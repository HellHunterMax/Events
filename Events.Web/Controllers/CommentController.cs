using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Extentions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Events.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService.ThrowIfNull(nameof(commentService));
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Comment>> Get()
        {
            var result = _commentService.GetAll();

            if (!result.Success) return NotFound();

            return Ok(result.Payload);
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> Get(Guid id)
        {
            var result = _commentService.Get(id);

            if (!result.Success) return NotFound(result.Exception!.Message);

            return Ok(result.Payload);
        }

        [HttpPost]
        public ActionResult<Comment> Post(CommentDto dto)
        {
            var result = _commentService.Create(dto);

            if (!result.Success) return BadRequest(result.Message);

            return result.Payload!;
        }

        [HttpPut]
        public ActionResult Put(CommentDto dto)
        {
            var result = _commentService.Update(dto);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult JsonPatchWithModelState(Guid id, [FromBody] JsonPatchDocument<CommentDto> patchDoc)
        {
            var result = _commentService.Get(id);

            if (!result.Success) return BadRequest(result.Message);

            var comment = result.Payload!;

            var commentDto = new CommentDto()
            {
                Id = comment.Id,
                Title = comment.Title,
                Text = comment.Text,
                EventId = comment.Event.Id,
                Status = comment.Status,
            };

            patchDoc.ApplyTo(commentDto);

            var updateResult = _commentService.Update(commentDto);

            if (!updateResult.Success) return BadRequest(updateResult.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _commentService.Delete(id);

            if (!result.Success) return NotFound(result.Message);

            return NoContent();
        }
    }
}
