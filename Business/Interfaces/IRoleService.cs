using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task<ServiceResponse<RoleDTO>> CreateRoleAsync(RoleDTO roleDTO);
        Task<ServiceResponse<bool>> DeleteRoleAsync(int roleId);
        Task<ServiceResponse<IEnumerable<RoleDTO>>> GetAllRolesAsync();
        Task<ServiceResponse<RoleDTO>> GetRoleByIdAsync(int roleId);
        Task<ServiceResponse<RoleDTO>> UpdateRoleAsync(int roleId, RoleDTO roleDTO);
    }
}