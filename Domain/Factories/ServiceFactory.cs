using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class ServiceFactory
    {
        public static ServiceDTO ToDTO(ServiceEntity service)
        {
            return new ServiceDTO
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                HourlyRate = service.HourlyRate
            };
        }

        public static ServiceEntity ToEntity(ServiceDTO serviceDTO)
        {
            return new ServiceEntity
            {
                Id = serviceDTO.Id,
                Name = serviceDTO.Name,
                Description = serviceDTO.Description,
                HourlyRate = serviceDTO.HourlyRate
            };
        }
    }
}
