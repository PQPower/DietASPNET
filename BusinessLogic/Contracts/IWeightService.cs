using DataAccessLayer.Entities;

namespace BusinessLogic.Contracts
{
    public interface IWeightService
    {
        public Task<int> CalculateCaloriesPerDayAsync(string userName);
        public Task AddUserWeightHistoryAsync(UserWeightHistory userWeightHistory);
    }
}
