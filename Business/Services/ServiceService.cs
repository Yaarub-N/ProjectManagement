﻿using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class ServiceService(IServiceRepository serviceRepository) : IServiceService
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;

        public async Task<ServiceResponse<ServiceDTO>> CreateServiceAsync(ServiceDTO serviceDTO)
        {
            try
            {
                if (serviceDTO == null)
                    return new ServiceResponse<ServiceDTO>(null!, false, "Invalid service data.");

                var serviceEntity = ServiceFactory.ToEntity(serviceDTO);
                var result = await _serviceRepository.AddAsync(serviceEntity);

                if (!result)
                    return new ServiceResponse<ServiceDTO>(null!, false, "Failed to create service.");

                return new ServiceResponse<ServiceDTO>(ServiceFactory.ToDTO(serviceEntity), true, "Service created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ServiceDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<ServiceDTO>> GetServiceByIdAsync(int serviceId)
        {
            try
            {
                if (serviceId <= 0)
                    return new ServiceResponse<ServiceDTO>(null!, false, "Invalid service ID.");

                var service = await _serviceRepository.GetAsync(s => s.Id == serviceId);
                if (service == null)
                    return new ServiceResponse<ServiceDTO>(null!, false, "Service not found.");

                return new ServiceResponse<ServiceDTO>(ServiceFactory.ToDTO(service), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<ServiceDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<ServiceDTO>>> GetAllServicesAsync()
        {
            try
            {
                var services = await _serviceRepository.GetAllAsync();
                if (!services.Any())
                    return new ServiceResponse<IEnumerable<ServiceDTO>>(null!, false, "No services found.");

                return new ServiceResponse<IEnumerable<ServiceDTO>>(ServiceFactory.ToDTOList(services), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<IEnumerable<ServiceDTO>>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<ServiceDTO>> UpdateServiceAsync(int serviceId, ServiceDTO serviceDTO)
        {
            try
            {
                if (serviceId <= 0 || serviceDTO == null)
                    return new ServiceResponse<ServiceDTO>(null!, false, "Invalid service update request.");

                var existingService = await _serviceRepository.GetAsync(s => s.Id == serviceId);
                if (existingService == null)
                    return new ServiceResponse<ServiceDTO>(null!, false, "Service not found.");

                existingService.Name = serviceDTO.Name;
                existingService.Description = serviceDTO.Description;
                existingService.HourlyRate = serviceDTO.HourlyRate;

                var result = await _serviceRepository.UpdateAsync(existingService);
                return result
                    ? new ServiceResponse<ServiceDTO>(ServiceFactory.ToDTO(existingService), true, "Service updated successfully.")
                    : new ServiceResponse<ServiceDTO>(null!, false, "Failed to update service.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ServiceDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteServiceAsync(int serviceId)
        {
            try
            {
                if (serviceId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid service ID.");

                var existingService = await _serviceRepository.GetAsync(s => s.Id == serviceId);
                if (existingService == null)
                    return new ServiceResponse<bool>(false, false, "Service not found.");

                var result = await _serviceRepository.RemoveAsync(existingService);
                return result
                    ? new ServiceResponse<bool>(true, true, "Service deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete service.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
