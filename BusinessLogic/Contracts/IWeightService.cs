using BusinessLogic.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;

namespace BusinessLogic.Contracts
{
    public interface IWeightService
    {
        public Task<int> CalculateCaloriesPerDayAsync(string userName);
        public Task AddUserWeightHistoryAsync(UserWeightHistory userWeightHistory);
        public Task<List<UserWeightHistory>> GetUserWeightHistoriesByUserIdAsync(int userId);
        public int CalculateCaloriesPerDay(DataToCalculate data);
        public int CalculateAgeFromBirthDate(DateTime birthDate);
    }
}
