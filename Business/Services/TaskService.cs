using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class TaskService(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;


        public async Task<ServiceResponse<TaskDTO>> CreateTaskAsync(TaskDTO taskDTO)
        {
            try
            {
                if (taskDTO == null)
                    return new ServiceResponse<TaskDTO>(null!, false, "Invalid task data.");

                var taskEntity = ProjectTaskFactory.ToEntity(taskDTO);  
                var result = await _taskRepository.AddAsync(taskEntity);

                if (!result)
                    return new ServiceResponse<TaskDTO>(null!, false, "Failed to create task.");

                return new ServiceResponse<TaskDTO>(ProjectTaskFactory.ToDTO(taskEntity), true, "Task created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<TaskDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<TaskDTO>> GetTaskByIdAsync(int taskId)
        {
            try
            {
                if (taskId <= 0)
                    return new ServiceResponse<TaskDTO>(null!, false, "Invalid task ID.");

                var task = await _taskRepository.GetAsync(t => t.Id == taskId);
                if (task == null)
                    return new ServiceResponse<TaskDTO>(null!, false, "Task not found.");

                return new ServiceResponse<TaskDTO>(ProjectTaskFactory.ToDTO(task), true);  
            }
            catch (Exception e)
            {
                return new ServiceResponse<TaskDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<TaskDTO>> UpdateTaskAsync(int taskId, TaskDTO taskDTO)
        {
            try
            {
                if (taskId <= 0 || taskDTO == null)
                    return new ServiceResponse<TaskDTO>(null!, false, "Invalid task update request.");

                var existingTask = await _taskRepository.GetAsync(t => t.Id == taskId);
                if (existingTask == null)
                    return new ServiceResponse<TaskDTO>(null!, false, "Task not found.");

                existingTask.Description = taskDTO.Description;
                existingTask.IsCompleted = taskDTO.IsCompleted;

                var result = await _taskRepository.UpdateAsync(existingTask);
                return result
                    ? new ServiceResponse<TaskDTO>(ProjectTaskFactory.ToDTO(existingTask), true, "Task updated successfully.")
                    : new ServiceResponse<TaskDTO>(null!, false, "Failed to update task.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<TaskDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteTaskAsync(int taskId)
        {
            try
            {
                if (taskId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid task ID.");

                var existingTask = await _taskRepository.GetAsync(t => t.Id == taskId);
                if (existingTask == null)
                    return new ServiceResponse<bool>(false, false, "Task not found.");

                var result = await _taskRepository.RemoveAsync(existingTask);
                return result
                    ? new ServiceResponse<bool>(true, true, "Task deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete task.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
