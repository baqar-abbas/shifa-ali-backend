using AF.Models;
using Microsoft.EntityFrameworkCore;
using AF.Data;

namespace AF.Repositories
{
    // DonationsRepository.cs
    public class DonationsRepository : IDonationsRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Donation> GetDonationByIdAsync(int id)
        {
            return await _context.Donations.Include(d => d.Fundraising).Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Donation>> GetDonationsByUserIdAsync(int userId)
        {
            return await _context.Donations.Include(d => d.Fundraising).Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task CreateDonationAsync(Donation donation)
        {
            // Ensure the User and Fundraising entities exist
            var user = await _context.Users.FindAsync(donation.UserId);
            var fundraising = await _context.Fundraising.FindAsync(donation.FundraisingId);

            if (user == null || fundraising == null)
            {
                throw new Exception("User or Fundraising not found.");
            }

            // Create the donation with the retrieved entities
            var completeDonation = new Donation
            {
                Amount = donation.Amount,
                Fundraising = fundraising,
                User = user,
                DonationDate = DateTime.Now,
                FundraisingId = donation.FundraisingId, // Map to correct column name
                UserId = donation.UserId                // Map to correct column name
            };

            // Add and save the donation
            await _context.Donations.AddAsync(completeDonation);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateDonationAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDonationAsync(int id)
        {
            var donation = await GetDonationByIdAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
