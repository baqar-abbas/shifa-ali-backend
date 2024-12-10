using AF.Models;

namespace AF.Repositories
{
    // IDonationsRepository.cs
    public interface IDonationsRepository
    {
        Task<Donation> GetDonationByIdAsync(int id);
        Task<IEnumerable<Donation>> GetDonationsByUserIdAsync(int userId);
        Task CreateDonationAsync(Donation donation);
        Task UpdateDonationAsync(Donation donation);
        Task DeleteDonationAsync(int id);
    }
}
