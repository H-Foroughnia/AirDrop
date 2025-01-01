using System.Security.Claims;
using Application.IService;
using Domain.DTOs.Label;
using Domain.IRepository;
using Domain.Models.Label;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Service;

public class LabelTaskService:ILabelTaskService
{
    private readonly ILabelTaskRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LabelTaskService> _logger;

    public LabelTaskService(ILabelTaskRepository repository, IHttpContextAccessor httpContextAccessor, ILogger<LabelTaskService> logger)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task DoLabelTaskAsync(LabelTaskDoneDto labelTask)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            _logger.LogWarning("User is not authenticated.");
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            _logger.LogWarning("User ID could not be retrieved from the token.");
            throw new UnauthorizedAccessException("User ID could not be retrieved from the token.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            _logger.LogWarning("User ID is not in a valid format.");
            throw new UnauthorizedAccessException("User ID is not in a valid format.");
        }

        _logger.LogInformation("Retrieved User ID from JWT: {UserId}", userId);
        _logger.LogInformation("TaskId: {TaskId}, Answer: {Answer}", labelTask.TaskId, labelTask.Answer);

        var labelDone = new LabelTaskDoneModel()
        {
            UserId = userId,
            TaskId = labelTask.TaskId,
            Answer = labelTask.Answer,
            StatusId = 2,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };
        await _repository.AddLabelTaskAsync(labelDone);
    }
}