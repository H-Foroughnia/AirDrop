using AirDrop.EndPoint.Areas.Admin.Controllers;
using Domain.IRepository;
using Domain.Models.Answer;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class AnswerSectionController : BaseController
    {
        private readonly IAnswerRepository _repository;

        public AnswerSectionController(IAnswerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Answer")]
        public async Task<IActionResult> Answer()
        {
            var answers = await _repository.GetAllAnswersAsync();
            return View(answers);
        }

        [HttpGet("AddAnswer")]
        public async Task<IActionResult> AddAnswer()
        {
            return View();
        }

        [HttpPost("AddAnswer")]
        public async Task<IActionResult> AddAnswer(AnswerModel model)
        {
            await _repository.AddAnswerAsync(model);
            TempData[SuccessMessage] = "Added Successfully.";
            return RedirectToAction("Answer");
        }
    }
}
