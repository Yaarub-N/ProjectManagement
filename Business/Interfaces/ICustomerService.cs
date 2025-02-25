using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResponse<CustomerDTO>> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<ServiceResponse<bool>> DeleteCustomerAsync(int customerId);
        Task<ServiceResponse<CustomerDTO>> GetCustomerByIdAsync(int customerId);
        Task<ServiceResponse<CustomerDTO>> UpdateCustomerAsync(int customerId, CustomerDTO customerDTO);
    }
}