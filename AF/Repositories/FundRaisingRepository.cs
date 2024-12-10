using AF.Models;
using Microsoft.EntityFrameworkCore;
using AF.Data;

namespace AF.Repositories
{
    // DonationsRepository.cs
    public class FundRaisingRepository : IFundRaisingRepository
    {
        private readonly ApplicationDbContext _context;

        public FundRaisingRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Fundraising> GetFundraisingsByIdAsync(int id)
        {
            return await _context.Fundraising.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Fundraising>> GetFundraisingsByUserIdAsync(int userId)
        {
            return await _context.Fundraising.ToListAsync();
        }

        public async Task CreateFundraisingAsync(Fundraising fundraising)
        {
            await _context.Fundraising.AddAsync(fundraising);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFundraisingAsync(Fundraising fundraising)
        {
            _context.Fundraising.Update(fundraising);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFundraisingAsync(int id)
        {
            var fundraising = await GetFundraisingsByIdAsync(id);
            if (fundraising != null)
            {
                _context.Fundraising.Remove(fundraising);
                await _context.SaveChangesAsync();
            }
        }
    }
}
