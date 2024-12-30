using AirDrop.EndPoint.Areas.Admin.Controllers;
using Domain.IRepository;
using Domain.Models.Status;
using Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class StatusSectionController : BaseController
    {
        private readonly IStatusRepository _repository;

        public StatusSectionController(IStatusRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("StatusTask")]
        public async Task<IActionResult> StatusTask()
        {
            var statuses = await _repository.GetAllStatusesAsync();
            return View(statuses);
        }

        [HttpGet("AddStatusTask")]
        public IActionResult AddStatusTask()
        {
            return View();
        }

        [HttpPost("AddStatusTask")]
        public async Task<IActionResult> AddStatusTask(StatusTaskModel model)
        {
            await _repository.AddStatusAsync(model);
            TempData[SuccessMessage] = "Added Successfully.";
            return RedirectToAction("StatusTask");
        }
    }
}
