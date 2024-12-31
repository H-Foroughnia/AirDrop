using Domain.Models.Answer;
using Domain.Models.Status;

namespace Domain.IRepository;

public interface IAnswerRepository
{
    Task<AnswerModel> AddAnswerAsync(AnswerModel answer);
    Task<IEnumerable<AnswerModel>> GetAllAnswersAsync();
}