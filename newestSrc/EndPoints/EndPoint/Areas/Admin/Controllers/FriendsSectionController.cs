using AirDrop.EndPoint.Areas.Admin.Controllers;
using Application.IService;
using Application.Service;
using Domain.IRepository;
using Domain.Models.Friend;
using Domain.Models.User;
using Domain.ViewModels;
using EndPoint.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class FriendsSectionController : BaseController
    {
        private readonly IFriendsService _service;
        private readonly IUserRepository _userRepository;

        public FriendsSectionController(IFriendsService service, IUserRepository userRepository)
        {
            _service = service;
            _userRepository = userRepository;
        }

        //[HttpGet("AddFriend")]
        //public async Task<IActionResult> AddFriend()
        //{
        //    var users = await _userRepository.GetAllUsersAsync();
        //    var viewModel = new UserAndUserListViewModel()
        //    {
        //        UserList = users
        //    };
        //    return View(viewModel);
        //}

        //[HttpPost("AddFriend")]
        //public async Task<IActionResult> AddFriend(FriendsModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _service.AddFriendAsync(model);
        //        TempData[SuccessMessage] = "Added Successfully";
        //        return RedirectToAction("AddFriend");
        //    }
        //    return View();
        //}

        [HttpGet("AddFriend")]
        public async Task<IActionResult> AddFriend()
        {
            var users = await _userRepository.GetAllUsersAsync();
            ViewBag.Users = users;
            return View();
        }

        [HttpPost("AddFriend")]
        public async Task<IActionResult> AddFriend(FriendsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.AddFriendAsync(viewModel);
                return RedirectToAction("Friends");
            }

            var users = await _userRepository.GetAllUsersAsync();
            ViewBag.Users = users;
            return View(viewModel);
        }

        [HttpGet("Friends")]
        public async Task<IActionResult> Friends()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var friends = await _service.GetAllFriendsAsync();
            ViewBag.Users = users;
            ViewBag.Friends = friends;
            return View();
        }
    }
}
