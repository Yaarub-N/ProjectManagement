using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class ProfileService(IProfileRepository profileRepository) : IProfileService
    {
        private readonly IProfileRepository _profileRepository = profileRepository;

        public async Task<ServiceResponse<ProfileDTO>> CreateProfileAsync(ProfileDTO profileDTO)
        {
            try
            {
                if (profileDTO == null)
                    return new ServiceResponse<ProfileDTO>(null!, false, "Invalid profile data.");

                var profileEntity = ProfileFactory.ToEntity(profileDTO);
                var result = await _profileRepository.AddAsync(profileEntity);

                if (!result)
                    return new ServiceResponse<ProfileDTO>(null!, false, "Failed to create profile.");

                return new ServiceResponse<ProfileDTO>(ProfileFactory.ToDTO(profileEntity), true, "Profile created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProfileDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProfileDTO>>> GetAllProfilesAsync()
        {
            try
            {
                var profiles = await _profileRepository.GetAllAsync(); 
                if (profiles == null || !profiles.Any())
                    return new ServiceResponse<IEnumerable<ProfileDTO>>(null!, false, "No profiles found.");

                var profileDTOs = ProfileFactory.ToDTOList(profiles); 
                return new ServiceResponse<IEnumerable<ProfileDTO>>(profileDTOs, true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<IEnumerable<ProfileDTO>>(null!, false, $"Something went wrong: {e.Message}");
            }
        }


        public async Task<ServiceResponse<ProfileDTO>> GetProfileByIdAsync(int profileId)
        {
            try
            {
                if (profileId <= 0)
                    return new ServiceResponse<ProfileDTO>(null!, false, "Invalid profile ID.");

                var profile = await _profileRepository.GetAsync(p => p.Id == profileId);
                if (profile == null)
                    return new ServiceResponse<ProfileDTO>(null!, false, "Profile not found.");

                return new ServiceResponse<ProfileDTO>(ProfileFactory.ToDTO(profile), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProfileDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<ProfileDTO>> UpdateProfileAsync(int profileId, ProfileDTO profileDTO)
        {
            try
            {
                if (profileId <= 0 || profileDTO == null)
                    return new ServiceResponse<ProfileDTO>(null!, false, "Invalid profile update request.");

                var existingProfile = await _profileRepository.GetAsync(p => p.Id == profileId);
                if (existingProfile == null)
                    return new ServiceResponse<ProfileDTO>(null!, false, "Profile not found.");

                existingProfile.Name = profileDTO.Name;
                existingProfile.LastName = profileDTO.LastName;
                existingProfile.ContactEmail = profileDTO.ContactEmail;
                existingProfile.PhoneNumber = profileDTO.PhoneNumber;

                var result = await _profileRepository.UpdateAsync(existingProfile);
                return result
                    ? new ServiceResponse<ProfileDTO>(ProfileFactory.ToDTO(existingProfile), true, "Profile updated successfully.")
                    : new ServiceResponse<ProfileDTO>(null!, false, "Failed to update profile.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProfileDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProfileAsync(int profileId)
        {
            try
            {
                if (profileId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid profile ID.");

                var existingProfile = await _profileRepository.GetAsync(p => p.Id == profileId);
                if (existingProfile == null)
                    return new ServiceResponse<bool>(false, false, "Profile not found.");

                var result = await _profileRepository.RemoveAsync(existingProfile);
                return result
                    ? new ServiceResponse<bool>(true, true, "Profile deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete profile.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
