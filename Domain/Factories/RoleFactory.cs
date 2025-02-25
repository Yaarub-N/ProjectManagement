using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class RoleFactory
    {
        public static RoleDTO ToDTO(RoleEntity role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }

        public static RoleEntity ToEntity(RoleDTO roleDTO)
        {
            return new RoleEntity
            {
                Id = roleDTO.Id,
                RoleName = roleDTO.RoleName
            };
        }
        public static IEnumerable<RoleDTO> ToDTOList(IEnumerable<RoleEntity> projects)
        {
            return projects.Select(ToDTO);
        }
    }

}
