using AF.Models;

namespace AF.Repositories
{
    public interface IFundRaisingRepository
    {
        Task<Fundraising> GetFundraisingsByIdAsync(int id);
        Task<IEnumerable<Fundraising>> GetFundraisingsByUserIdAsync(int userId);
        Task CreateFundraisingAsync(Fundraising fundraising);
        Task UpdateFundraisingAsync(Fundraising fundraising);
        Task DeleteFundraisingAsync(int id);
    }
}
