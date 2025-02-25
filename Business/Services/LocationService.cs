using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class LocationService(ILocationRepository locationRepository)
    {
        private readonly ILocationRepository _locationRepository = locationRepository;

        public async Task<ServiceResponse<LocationDTO>> CreateLocationAsync(LocationDTO locationDTO)
        {
            try
            {
                if (locationDTO == null)
                    return new ServiceResponse<LocationDTO>(null!, false, "Invalid location data.");
                var locationEntity = LocationFactory.ToEntity(locationDTO);
                var result = await _locationRepository.AddAsync(locationEntity);

                if (!result)
                    return new ServiceResponse<LocationDTO>(null!, false, "Failed to create location.");

                return new ServiceResponse<LocationDTO>(LocationFactory.ToDTO(locationEntity), true, "Location created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<LocationDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<LocationDTO>> GetLocationByIdAsync(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return new ServiceResponse<LocationDTO>(null!, false, "Invalid location ID.");

                var location = await _locationRepository.GetAsync(l => l.Id == locationId);
                if (location == null)
                    return new ServiceResponse<LocationDTO>(null!, false, "Location not found.");

                return new ServiceResponse<LocationDTO>(LocationFactory.ToDTO(location), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<LocationDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<LocationDTO>> UpdateLocationAsync(int locationId, LocationDTO locationDTO)
        {
            try
            {
                if (locationId <= 0 || locationDTO == null)
                    return new ServiceResponse<LocationDTO>(null!, false, "Invalid location update request.");

                var existingLocation = await _locationRepository.GetAsync(l => l.Id == locationId);
                if (existingLocation == null)
                    return new ServiceResponse<LocationDTO>(null!, false, "Location not found.");

                existingLocation.City = locationDTO.City;
                existingLocation.PostalCode = locationDTO.PostalCode;
                existingLocation.Country = locationDTO.Country;

                var result = await _locationRepository.UpdateAsync(existingLocation);
                return result
                    ? new ServiceResponse<LocationDTO>(LocationFactory.ToDTO(existingLocation), true, "Location updated successfully.")
                    : new ServiceResponse<LocationDTO>(null!, false, "Failed to update location.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<LocationDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteLocationAsync(int locationId)
        {
            try
            {
                if (locationId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid location ID.");

                var existingLocation = await _locationRepository.GetAsync(l => l.Id == locationId);
                if (existingLocation == null)
                    return new ServiceResponse<bool>(false, false, "Location not found.");

                var result = await _locationRepository.RemoveAsync(existingLocation);
                return result
                    ? new ServiceResponse<bool>(true, true, "Location deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete location.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
