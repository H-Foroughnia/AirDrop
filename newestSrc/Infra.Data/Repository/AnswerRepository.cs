using Domain.IRepository;
using Domain.Models.Answer;
using Domain.Models.Status;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class AnswerRepository:IAnswerRepository
{
    private readonly AppDbContext _context;

    public AnswerRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<AnswerModel> AddAnswerAsync(AnswerModel answer)
    {
        await _context.Answers.AddAsync(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<IEnumerable<AnswerModel>> GetAllAnswersAsync()
    {
        return await _context.Answers.ToListAsync();
    }
}