using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;

namespace BusinessLogic.Implementations
{
    public class WeightService : IWeightService
    {
        private readonly IUserService _userService;
        private readonly IDbRepository _db;

        public WeightService(IUserService userService, IDbRepository db)
        {
            this._userService = userService;
            this._db = db;
        }

        public async Task AddUserWeightHistoryAsync(UserWeightHistory userWeightHistory)
        {
            await _db.Add(userWeightHistory);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CalculateCaloriesPerDayAsync(string userName)
        {
            var user = await _userService.GetUserByNameAsync(userName);
            var userAge = DateTime.Today.Year - user.BirthDate.Year; // 2023 - 2000 = 23
            if (user.BirthDate.Date > DateTime.Today.AddYears(-userAge)) // 5 декабря 2000 > 13 марта 2000 
                userAge--; // 23 - 1 = 22
            var coefficient = user.LifeStyle switch
            {
                LifeStyle.Passive => 1.2,
                LifeStyle.Low => 1.375,
                LifeStyle.Medium => 1.55,
                LifeStyle.High => 1.7,
                LifeStyle.VeryHigh => 1.9,
                _ => throw new Exception("no coefficient"),
            };
            if (user.Gender == Gender.Male)
            {
                return Convert.ToInt32((10 * user.UserWeightHistorys.Last().Weight + 6.25 * user.Height - 5 * userAge + 5) * coefficient);
            }
            else
            {
                return Convert.ToInt32((10 * user.UserWeightHistorys.Last().Weight + 6.25 * user.Height - 5 * userAge - 161) * coefficient);
            }
        }
    }
}
