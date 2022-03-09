using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Exceptions;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.OperationResults;
using Microsoft.Extensions.Logging;

namespace Events.Core.Services
{
    public class OfficeService : IOfficeService
    {
        public readonly IRepository<Office> _officeRepository;
        private readonly ILogger<CategoryService> _logger;

        public OfficeService(IRepository<Office> officeRepository, ILogger<CategoryService> logger)
        {
            _officeRepository = officeRepository.ThrowIfNull(nameof(officeRepository));
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        public OperationResult<Office> Create(OfficeDto dto)
        {
            try
            {
                var office = new Office(dto.Name, dto.Discription, dto.Location, dto.Email, dto.PhoneNumber, dto.Status);
                _officeRepository.Add(office);

                return OperationResult<Office>.Succeeded(office);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Office>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                var office = _officeRepository.GetById(id);

                if (office is null) throw new NotFoundException($"Office with given Id not found Id was: {id}");

                _officeRepository.Delete(office);

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }
        }

        public OperationResult<Office> Get(Guid id)
        {
            try
            {
                var office = _officeRepository.GetById(id);

                return OperationResult<Office>.Succeeded(office);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Office>.Failed(ex, ex.Message);
            }
        }

        public OperationResult<IReadOnlyCollection<Office>> GetAll()
        {
            try
            {
                var offices = _officeRepository.List();

                return OperationResult<IReadOnlyCollection<Office>>.Succeeded(offices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<IReadOnlyCollection<Office>>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Update(OfficeDto category)
        {
            try
            {
                var dbOffice = _officeRepository.GetById(category.Id);
                if (dbOffice is null) throw new NotFoundException($"No category found with id: {category.Id}");

                dbOffice.SetName(category.Name);
                dbOffice.SetDescription(category.Discription);
                dbOffice.SetLocation(category.Location);
                dbOffice.SetEmail(category.Email);
                dbOffice.SetPhoneNumber(category.PhoneNumber);
                dbOffice.SetStatus(category.Status);

                _officeRepository.Edit(dbOffice);

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
