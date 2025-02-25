using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IProfileService
    {
        Task<ServiceResponse<ProfileDTO>> CreateProfileAsync(ProfileDTO profileDTO);
        Task<ServiceResponse<bool>> DeleteProfileAsync(int profileId);
        Task<ServiceResponse<ProfileDTO>> GetProfileByIdAsync(int profileId);
        Task<ServiceResponse<ProfileDTO>> UpdateProfileAsync(int profileId, ProfileDTO profileDTO);
    }
}