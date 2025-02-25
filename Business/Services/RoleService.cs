using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<ServiceResponse<RoleDTO>> CreateRoleAsync(RoleDTO roleDTO)
        {
            try
            {
                if (roleDTO == null)
                    return new ServiceResponse<RoleDTO>(null!, false, "Invalid role data.");

                var roleEntity = RoleFactory.ToEntity(roleDTO);
                var result = await _roleRepository.AddAsync(roleEntity);

                if (!result)
                    return new ServiceResponse<RoleDTO>(null!, false, "Failed to create role.");

                return new ServiceResponse<RoleDTO>(RoleFactory.ToDTO(roleEntity), true, "Role created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<RoleDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<RoleDTO>> GetRoleByIdAsync(int roleId)
        {
            try
            {
                if (roleId <= 0)
                    return new ServiceResponse<RoleDTO>(null!, false, "Invalid role ID.");

                var role = await _roleRepository.GetAsync(r => r.Id == roleId);
                if (role == null)
                    return new ServiceResponse<RoleDTO>(null!, false, "Role not found.");

                return new ServiceResponse<RoleDTO>(RoleFactory.ToDTO(role), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<RoleDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<RoleDTO>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                if (!roles.Any())
                    return new ServiceResponse<IEnumerable<RoleDTO>>(null!, false, "No roles found.");

                return new ServiceResponse<IEnumerable<RoleDTO>>(RoleFactory.ToDTOList(roles), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<IEnumerable<RoleDTO>>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<RoleDTO>> UpdateRoleAsync(int roleId, RoleDTO roleDTO)
        {
            try
            {
                if (roleId <= 0 || roleDTO == null)
                    return new ServiceResponse<RoleDTO>(null!, false, "Invalid role update request.");

                var existingRole = await _roleRepository.GetAsync(r => r.Id == roleId);
                if (existingRole == null)
                    return new ServiceResponse<RoleDTO>(null!, false, "Role not found.");

                existingRole.RoleName = roleDTO.RoleName;

                var result = await _roleRepository.UpdateAsync(existingRole);
                return result
                    ? new ServiceResponse<RoleDTO>(RoleFactory.ToDTO(existingRole), true, "Role updated successfully.")
                    : new ServiceResponse<RoleDTO>(null!, false, "Failed to update role.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<RoleDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteRoleAsync(int roleId)
        {
            try
            {
                if (roleId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid role ID.");

                var existingRole = await _roleRepository.GetAsync(r => r.Id == roleId);
                if (existingRole == null)
                    return new ServiceResponse<bool>(false, false, "Role not found.");

                var result = await _roleRepository.RemoveAsync(existingRole);
                return result
                    ? new ServiceResponse<bool>(true, true, "Role deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete role.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
