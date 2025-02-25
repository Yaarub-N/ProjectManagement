using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class ProfileFactory
    {
        public static ProfileDTO ToDTO(ProfileEntity profile)
        {
            return new ProfileDTO
            {
                Id = profile.Id,
                Name = profile.Name,
                LastName = profile.LastName ?? "No Last Name",  
                ContactEmail = profile.ContactEmail,
                PhoneNumber = profile.PhoneNumber ?? "No Phone Number"  
            };
        }
        public static ProfileEntity ToEntity(ProfileDTO profileDTO)
        {
            return new ProfileEntity
            {
                Id = profileDTO.Id,
                Name = profileDTO.Name,
                LastName = profileDTO.LastName,
                ContactEmail = profileDTO.ContactEmail,
                PhoneNumber = profileDTO.PhoneNumber
            };
        }
        public static IEnumerable<ProfileDTO> ToDTOList(IEnumerable<ProfileEntity> profiles)
        {
            return profiles.Select(ToDTO);
        }
    }
}
