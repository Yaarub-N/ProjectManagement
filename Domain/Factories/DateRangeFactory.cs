using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class DateRangeFactory
    {
        public static DateRangeDTO ToDTO(DateRangeEntity dateRange)
        {
            return new DateRangeDTO
            {
                Id = dateRange.Id,
                StartDate = dateRange.StartDate,
                EndDate = dateRange.EndDate
            };
        }

        public static DateRangeEntity ToEntity(DateRangeDTO dateRangeDTO)
        {
            return new DateRangeEntity
            {
                Id = dateRangeDTO.Id,
                StartDate = dateRangeDTO.StartDate,
                EndDate = dateRangeDTO.EndDate
            };
        }
    }
}
