using Events.Core.Dtos;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.Exceptions;
using Events.Core.Shared.Extentions;
using Events.Core.Shared.OperationResults;
using Events.Core.Specifications.CommentSpecifications;
using Microsoft.Extensions.Logging;

namespace Events.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IEventService _eventService;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IRepository<Comment> commentRepository, ILogger<CommentService> logger, IEventService eventService)
        {
            _commentRepository = commentRepository.ThrowIfNull(nameof(commentRepository));
            _logger = logger.ThrowIfNull(nameof(logger));
            _eventService = eventService.ThrowIfNull(nameof(eventService));
        }

        public OperationResult<Comment> Create(CommentDto dto)
        {
            try
            {
                var @event = _eventService.Get(dto.EventId);
                if (!@event.Success) throw new NotFoundException($"No office Found for Id: {dto.EventId}");

                var comment = new Comment(dto.Title, dto.Text, @event.Payload!, dto.Status);
                _commentRepository.Add(comment);

                return OperationResult<Comment>.Succeeded(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Comment>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Delete(Guid id)
        {
            try
            {
                var comment = _commentRepository.GetById(id);
                if (comment is null) throw new NotFoundException(nameof(comment));

                _commentRepository.Delete(comment);

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }
        }

        public OperationResult<Comment> Get(Guid id)
        {
            try
            {
                var comment = _commentRepository.List(new GetCommentByIdAndIncludeSpecification(id)).SingleOrDefault();
                if (comment is null) throw new NotFoundException($"No Comment found with id: {id}");

                return OperationResult<Comment>.Succeeded(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<Comment>.Failed(ex, ex.Message);
            }
        }

        public OperationResult<IReadOnlyCollection<Comment>> GetAll()
        {
            try
            {
                var comments = _commentRepository.List();

                return OperationResult<IReadOnlyCollection<Comment>>.Succeeded(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult<IReadOnlyCollection<Comment>>.Failed(ex, ex.Message);
            }
        }

        public OperationResult Update(CommentDto dto)
        {
            try
            {
                var comment = _commentRepository.List(new GetCommentByIdAndIncludeSpecification(dto.Id)).SingleOrDefault();
                if (comment is null) throw new NotFoundException($"No Comment found with id: {dto.Id}");

                comment.SetTitle(dto.Title);
                comment.SetText(dto.Text);
                comment.SetStatus(dto.Status);

                _commentRepository.Edit(comment);

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
