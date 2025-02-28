using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfileController(ProfileService profileService) : ControllerBase
    {
        private readonly ProfileService _profileService = profileService;

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileDTO profileDTO)
        {
            var response = await _profileService.CreateProfileAsync(profileDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var response = await _profileService.GetAllProfilesAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetProfile(int profileId)
        {
            var response = await _profileService.GetProfileByIdAsync(profileId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateProfile(int profileId, [FromBody] ProfileDTO profileDTO)
        {
            var response = await _profileService.UpdateProfileAsync(profileId, profileDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            var response = await _profileService.DeleteProfileAsync(profileId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
