using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Exceptions;
using Events.Core.Shared.OperationResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Events.Core.Services
{

    public class UserService : IUserService
    {

        public UserService(ILogger<UserService> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserService> _logger;

        public async Task<OperationResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) throw new NotFoundException($"No category found with id: {id}");

                await _userManager.DeleteAsync(user);

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }
        }

        public async Task<OperationResult<UserReplyDto>> Get(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) throw new NotFoundException($"No category found with id: {id}");

                UserReplyDto dto = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return OperationResult<UserReplyDto>.Succeeded(dto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<UserReplyDto>.Failed(ex, ex.Message);
            }
        }

        public OperationResult<IReadOnlyCollection<UserReplyDto>> GetAll()
        {
            try
            {
                var users = _userManager.Users.ToList().AsReadOnly();

                List<UserReplyDto> userReplyDtos = new();

                foreach (var user in users)
                {
                    UserReplyDto dto = new()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    };
                    userReplyDtos.Add(dto);
                }

                return OperationResult<IReadOnlyCollection<UserReplyDto>>.Succeeded(userReplyDtos.AsReadOnly());
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<IReadOnlyCollection<UserReplyDto>>.Failed(ex, ex.Message);
            }
        }

        public async Task<OperationResult> Update(UserUpdateDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.Id);
                if (user is null) throw new NotFoundException($"No category found with id: {dto.Id}");

                user.SetFirstName(dto.FirstName);
                user.SetLastName(dto.LastName);
                user.SetEmail(dto.Email);
                user.SetPhoneNumber(dto.PhoneNumber);

                await _userManager.UpdateAsync(user);

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
