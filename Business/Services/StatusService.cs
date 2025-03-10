﻿using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class StatusService(IStatusRepository statusRepository) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;

        public async Task<ServiceResponse<StatusDTO>> CreateStatusAsync(StatusDTO statusDTO)
        {
            try
            {
                if (statusDTO == null)
                    return new ServiceResponse<StatusDTO>(null!, false, "Invalid status data.");

                var statusEntity = StatusFactory.ToEntity(statusDTO);
                var result = await _statusRepository.AddAsync(statusEntity);

                if (!result)
                    return new ServiceResponse<StatusDTO>(null!, false, "Failed to create status.");

                return new ServiceResponse<StatusDTO>(StatusFactory.ToDTO(statusEntity), true, "Status created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<StatusDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }
    
        public async Task<ServiceResponse<IEnumerable<StatusDTO>>> GetAllStatusesAsync()
        {
            try
            {
                var statuses = await _statusRepository.GetAllAsync();
                if (!statuses.Any())
                    return new ServiceResponse<IEnumerable<StatusDTO>>(null!, false, "No statuses found.");

                return new ServiceResponse<IEnumerable<StatusDTO>>(StatusFactory.ToDTOList(statuses), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<IEnumerable<StatusDTO>>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<StatusDTO>> GetStatusByIdAsync(int statusId)
        {
            try
            {
                if (statusId <= 0)
                    return new ServiceResponse<StatusDTO>(null!, false, "Invalid status ID.");

                var status = await _statusRepository.GetAsync(s => s.Id == statusId);
                if (status == null)
                    return new ServiceResponse<StatusDTO>(null!, false, "Status not found.");

                return new ServiceResponse<StatusDTO>(StatusFactory.ToDTO(status), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<StatusDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }


        public async Task<ServiceResponse<StatusDTO>> UpdateStatusAsync(int statusId, StatusDTO statusDTO)
        {
            try
            {
                if (statusId <= 0 || statusDTO == null)
                    return new ServiceResponse<StatusDTO>(null!, false, "Invalid status update request.");

                var existingStatus = await _statusRepository.GetAsync(s => s.Id == statusId);
                if (existingStatus == null)
                    return new ServiceResponse<StatusDTO>(null!, false, "Status not found.");

                existingStatus.Name = statusDTO.Name;

                var result = await _statusRepository.UpdateAsync(existingStatus);
                return result
                    ? new ServiceResponse<StatusDTO>(StatusFactory.ToDTO(existingStatus), true, "Status updated successfully.")
                    : new ServiceResponse<StatusDTO>(null!, false, "Failed to update status.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<StatusDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteStatusAsync(int statusId)
        {
            try
            {
                if (statusId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid status ID.");

                var existingStatus = await _statusRepository.GetAsync(s => s.Id == statusId);
                if (existingStatus == null)
                    return new ServiceResponse<bool>(false, false, "Status not found.");

                var result = await _statusRepository.RemoveAsync(existingStatus);
                return result
                    ? new ServiceResponse<bool>(true, true, "Status deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete status.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
