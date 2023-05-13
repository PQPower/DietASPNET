using BusinessLogic.Contracts;
using DietTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietTracker.Controllers
{
    public class WeightController : Controller
    {

        private readonly IWeightService _weightService;

        public WeightController(IWeightService weightService)
        {
            this._weightService = weightService;
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new WeightViewModel
            {
                CaloriesPerDay = await _weightService.CalculateCaloriesPerDayAsync(User.Identity.Name)
            };
            return View(model);
        }
    }
}
