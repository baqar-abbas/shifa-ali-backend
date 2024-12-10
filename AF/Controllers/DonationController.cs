using AF.Models;
using AF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AF.Controllers
{

   

    // DonationsController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly IDonationsRepository _donationsRepository;

        public DonationsController(IDonationsRepository donationsRepository)
        {
            _donationsRepository = donationsRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Donation>> GetDonation(int id)
        {
            var donation = await _donationsRepository.GetDonationByIdAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return Ok(donation);
        }

        [HttpPost]
        public async Task<ActionResult<Donation>> CreateDonation([FromBody] DonationCreateDto donationDto)
        {
            // Map DTO to Donation entity
            var donation = new Donation
            {
                FundraisingId = donationDto.FundraisingId,
                UserId = donationDto.UserId,
                Amount = donationDto.Amount,
                DonationDate = donationDto.DonationDate
            };

            await _donationsRepository.CreateDonationAsync(donation);

            // Return the created donation with its ID
            return CreatedAtAction(nameof(GetDonation), new { id = donation.Id }, donation);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonation(int id, [FromBody] Donation donation)
        {
            if (id != donation.Id)
            {
                return BadRequest();
            }

            await _donationsRepository.UpdateDonationAsync(donation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            await _donationsRepository.DeleteDonationAsync(id);
            return NoContent();
        }
    }
}
