using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService _roleService = roleService;

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
        {
            var response = await _roleService.CreateRoleAsync(roleDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(int roleId)
        {
            var response = await _roleService.GetRoleByIdAsync(roleId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleDTO roleDTO)
        {
            var response = await _roleService.UpdateRoleAsync(roleId, roleDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            var response = await _roleService.DeleteRoleAsync(roleId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
