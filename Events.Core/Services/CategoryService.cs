using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Exceptions;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.OperationResults;
using Microsoft.Extensions.Logging;

namespace Events.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _CategoryRepo;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IRepository<Category> categoryRepository, ILogger<CategoryService> logger)
        {
            _CategoryRepo = categoryRepository.ThrowIfNull(nameof(categoryRepository));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        public OperationResult<Category> Create(string name)
        {
            try
            {
                Category category = new(name);
                _CategoryRepo.Add(category);

                return OperationResult<Category>.Succeeded(category);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Category>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                var category = Get(id).Payload;
                if (category is null) throw new NotFoundException($"No category found with id: {id}");

                _CategoryRepo.Delete(category);

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }
        }

        public OperationResult<Category> Get(Guid id)
        {
            try
            {
                var category = _CategoryRepo.GetById(id);
                if (category is null) throw new NotFoundException($"No category found with id: {id}");

                return OperationResult<Category>.Succeeded(category);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Category>.Failed(ex, ex.Message);
            }
        }

        public OperationResult<IReadOnlyCollection<Category>> GetAll()
        {
            try
            {
                var category = _CategoryRepo.List();

                return OperationResult<IReadOnlyCollection<Category>>.Succeeded(category);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<IReadOnlyCollection<Category>>.Failed(ex, ex.Message);
            }
        }


        public OperationResult Update(CategoryDto category)
        {
            try
            {
                var dbCategory = _CategoryRepo.GetById(category.Id);
                if (dbCategory is null) throw new NotFoundException($"No category found with id: {category.Id}");

                dbCategory.SetName(category.Name);
                dbCategory.SetStatus(category.Status);

                _CategoryRepo.Edit(dbCategory);

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
