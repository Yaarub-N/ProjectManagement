using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class DateRangeService(IDateRangeRepository dateRangeRepository) : IDateRangeService
    {
        private readonly IDateRangeRepository _dateRangeRepository = dateRangeRepository;

        public async Task<ServiceResponse<DateRangeDTO>> CreateDateRangeAsync(DateRangeDTO dateRangeDTO)
        {
            try
            {
                if (dateRangeDTO == null)
                    return new ServiceResponse<DateRangeDTO>(null!, false, "Invalid DateRange data.");

                var dateRangeEntity = DateRangeFactory.ToEntity(dateRangeDTO);
                var result = await _dateRangeRepository.AddAsync(dateRangeEntity);

                if (!result)
                    return new ServiceResponse<DateRangeDTO>(null!, false, "Failed to create DateRange.");

                return new ServiceResponse<DateRangeDTO>(DateRangeFactory.ToDTO(dateRangeEntity), true, "DateRange created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<DateRangeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<DateRangeDTO>> GetDateRangeByIdAsync(int dateRangeId)
        {
            try
            {
                if (dateRangeId <= 0)
                    return new ServiceResponse<DateRangeDTO>(null!, false, "Invalid DateRange ID.");

                var dateRange = await _dateRangeRepository.GetAsync(dr => dr.Id == dateRangeId);
                if (dateRange == null)
                    return new ServiceResponse<DateRangeDTO>(null!, false, "DateRange not found.");

                return new ServiceResponse<DateRangeDTO>(DateRangeFactory.ToDTO(dateRange), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<DateRangeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<DateRangeDTO>> UpdateDateRangeAsync(int dateRangeId, DateRangeDTO dateRangeDTO)
        {
            try
            {
                if (dateRangeId <= 0 || dateRangeDTO == null)
                    return new ServiceResponse<DateRangeDTO>(null!, false, "Invalid DateRange update request.");

                var existingDateRange = await _dateRangeRepository.GetAsync(dr => dr.Id == dateRangeId);
                if (existingDateRange == null)
                    return new ServiceResponse<DateRangeDTO>(null!, false, "DateRange not found.");

                existingDateRange.StartDate = dateRangeDTO.StartDate;
                existingDateRange.EndDate = dateRangeDTO.EndDate;

                var result = await _dateRangeRepository.UpdateAsync(existingDateRange);
                return result
                    ? new ServiceResponse<DateRangeDTO>(DateRangeFactory.ToDTO(existingDateRange), true, "DateRange updated successfully.")
                    : new ServiceResponse<DateRangeDTO>(null!, false, "Failed to update DateRange.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<DateRangeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteDateRangeAsync(int dateRangeId)
        {
            try
            {
                if (dateRangeId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid DateRange ID.");

                var existingDateRange = await _dateRangeRepository.GetAsync(dr => dr.Id == dateRangeId);
                if (existingDateRange == null)
                    return new ServiceResponse<bool>(false, false, "DateRange not found.");

                var result = await _dateRangeRepository.RemoveAsync(existingDateRange);
                return result
                    ? new ServiceResponse<bool>(true, true, "DateRange deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete DateRange.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
