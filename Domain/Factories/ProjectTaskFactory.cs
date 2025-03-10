﻿using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class ProjectTaskFactory  
    {
        public static TaskDTO ToDTO(TaskEntity task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                ProjectId = task.ProjectId
            };
        }

        public static TaskEntity ToEntity(TaskDTO taskDTO)
        {
            return new TaskEntity
            {
                Id = taskDTO.Id,
                Description = taskDTO.Description,
                IsCompleted = taskDTO.IsCompleted,
                ProjectId = taskDTO.ProjectId
            };
        }
        public static IEnumerable<TaskDTO> ToDTOList(IEnumerable<TaskEntity> profiles)
        {
            return profiles.Select(ToDTO);
        }
    }
}
