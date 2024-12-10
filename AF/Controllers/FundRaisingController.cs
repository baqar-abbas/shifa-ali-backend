using AF.Models;
using AF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AF.Controllers
{

    // DonationsController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class FundRaisingController : ControllerBase
    {
        private readonly IFundRaisingRepository _fundraisingRepository;

        public FundRaisingController(IFundRaisingRepository fundraisingRepository)
        {
            _fundraisingRepository = fundraisingRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fundraising>> GetDonation(int id)
        {
            var fundraising = await _fundraisingRepository.GetFundraisingsByIdAsync(id);
            if (fundraising == null)
            {
                return NotFound();
            }
            return Ok(fundraising);
        }

        [HttpPost]
        public async Task<ActionResult<Fundraising>> CreateDonation( Fundraising fundraising)
        {
         

            await _fundraisingRepository.CreateFundraisingAsync(fundraising);

            // Return the created donation with its ID
            return CreatedAtAction(nameof(GetDonation), new { id = fundraising.Id }, fundraising);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonation(int id, [FromBody] Fundraising donation)
        {
            if (id != donation.Id)
            {
                return BadRequest();
            }

            await _fundraisingRepository.UpdateFundraisingAsync(donation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            await _fundraisingRepository.DeleteFundraisingAsync(id);
            return NoContent();
        }
    }
}
