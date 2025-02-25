using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class StatusFactory
    {
        public static StatusDTO ToDTO(StatusEntity status)
        {
            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public static StatusEntity ToEntity(StatusDTO statusDTO)
        {
            return new StatusEntity
            {
                Id = statusDTO.Id,
                Name = statusDTO.Name
            };
        }
        public static IEnumerable<StatusDTO> ToDTOList(IEnumerable<StatusEntity> projects)
        {
            return projects.Select(ToDTO);
        }
    }
}
