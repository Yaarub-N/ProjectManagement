using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface ITaskService
    {
        Task<ServiceResponse<TaskDTO>> CreateTaskAsync(TaskDTO taskDTO);
        Task<ServiceResponse<bool>> DeleteTaskAsync(int taskId);
        Task<ServiceResponse<TaskDTO>> GetTaskByIdAsync(int taskId);
        Task<ServiceResponse<TaskDTO>> UpdateTaskAsync(int taskId, TaskDTO taskDTO);
    }
}