using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Events.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(Guid id)
        {
            var result = _categoryService.Get(id);

            if (!result.Success) return NotFound(result.Exception!.Message);

            return Ok(result.Payload);
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Category>> Get()
        {
            var result = _categoryService.GetAll();

            if (!result.Success) return NotFound();

            return Ok(result.Payload);
        }

        [HttpPost]
        public ActionResult<Category> Post(string name)
        {
            var result = _categoryService.Create(name);

            if (!result.Success) return BadRequest(result.Message);

            return result.Payload!;
        }

        [HttpPut]
        public ActionResult Put(CategoryDto category)
        {
            var result = _categoryService.Update(category);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult JsonPatchWithModelState(Guid id, [FromBody] JsonPatchDocument<CategoryDto> patchDoc)
        {
            var result = _categoryService.Get(id);

            if (!result.Success) return BadRequest(result.Message);

            var category = result.Payload!;

            CategoryDto categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Status = category.Status,
            };

            patchDoc.ApplyTo(categoryDto);

            var updateResult = _categoryService.Update(categoryDto);

            if (!updateResult.Success) return BadRequest(updateResult.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _categoryService.Delete(id);

            if (!result.Success) return NotFound(result.Message);

            return NoContent();
        }
    }
}
