using BusinessLogic;
using BusinessLogic.Contracts;
using BusinessLogic.Implementations;
using BusinessLogic.Models;
using DataAccessLayer.Entities;
using DietTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DietTracker.Controllers
{
    public class WeightController : Controller
    {

        private readonly IWeightService _weightService;
        private readonly IUserService _userService;


        public WeightController(IWeightService weightService, IUserService userService)
        {
            _weightService = weightService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //todo add identity to get ID the proper way
                var userData = await _userService.GetUserByNameAsync(User.Identity.Name);
                var model = new WeightViewModel()
                {
                    Height = userData.Height,
                    Age = _weightService.CalculateAgeFromBirthDate(userData.BirthDate),
                    Weight = userData.UserWeightHistorys.Last().Weight,
                    Gender = userData.Gender,
                    LifeStyle = userData.LifeStyle,
                    Expectation = userData.Expectation,
                };
                return View(model);
            } 
            return View();

        }
        [HttpPost]
        public IActionResult Calculate(WeightViewModel viewModel)
        {
            //TODO add mapper
            var model = new DataToCalculate()
            {
                Height = viewModel.Height,
                Age = viewModel.Age,
                Weight = viewModel.Weight,
                Gender = viewModel.Gender,
                LifeStyle = viewModel.LifeStyle,
                Expectation = viewModel.Expectation,
            };
            viewModel.CaloriesPerDay = _weightService.CalculateCaloriesPerDay(model);
            ModelState.Clear();
            return View("Index", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> WeightGraph()
        {
            int userId = (await _userService.GetUserByNameAsync(User.Identity.Name)).Id;
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var dataFromDb = (await _weightService.GetUserWeightHistoriesByUserIdAsync(userId)).Select(
                x => new WeightGraphViewModel() { UpdatedDate = x.UpdatedDate.ToString("dd.MM.yy"), Weight = x.Weight });
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataFromDb, _jsonSetting);
            return View();
        }
        public async Task<IActionResult> AddWeight(double weight)
        {
            var userData = await _userService.GetUserByNameAsync(User.Identity.Name);
            var model = new UserWeightHistory()
            {
                UserId = userData.Id,
                Weight = weight,
                UpdatedDate = DateTime.Now,
          
            };
            await _weightService.AddUserWeightHistoryAsync(model);
            return RedirectToAction("WeightGraph");
        }
    }
}
