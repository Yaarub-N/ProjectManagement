using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<ServiceResponse<CustomerDTO>> CreateCustomerAsync(CustomerDTO customerDTO)
        {
            try
            {
                if (customerDTO == null)
                    return new ServiceResponse<CustomerDTO>(null!, false, "Invalid customer data.");

                var customerEntity = CustomerFactory.ToEntity(customerDTO);
                var result = await _customerRepository.AddAsync(customerEntity);

                if (!result)
                    return new ServiceResponse<CustomerDTO>(null!, false, "Failed to create customer.");

                return new ServiceResponse<CustomerDTO>(CustomerFactory.ToDTO(customerEntity), true, "Customer created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<CustomerDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }


        public async Task<ServiceResponse<CustomerDTO>> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                if (customerId <= 0)
                    return new ServiceResponse<CustomerDTO>(null!, false, "Invalid customer ID.");

                var customer = await _customerRepository.GetAsync(c => c.Id == customerId);
                if (customer == null)
                    return new ServiceResponse<CustomerDTO>(null!, false, "Customer not found.");

                return new ServiceResponse<CustomerDTO>(CustomerFactory.ToDTO(customer), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<CustomerDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<CustomerDTO>> UpdateCustomerAsync(int customerId, CustomerDTO customerDTO)
        {
            try
            {
                if (customerId <= 0 || customerDTO == null)
                    return new ServiceResponse<CustomerDTO>(null!, false, "Invalid customer update request.");

                var existingCustomer = await _customerRepository.GetAsync(c => c.Id == customerId);
                if (existingCustomer == null)
                    return new ServiceResponse<CustomerDTO>(null!, false, "Customer not found.");

                var updatedCustomer = CustomerFactory.ToEntity(customerDTO);

                existingCustomer.Profile!.Name = updatedCustomer.Profile!.Name;
                existingCustomer.Profile.ContactEmail = updatedCustomer.Profile.ContactEmail;
                existingCustomer.Address!.Street = updatedCustomer.Address!.Street;
                existingCustomer.Address.Location!.City = updatedCustomer.Address.Location!.City;
                existingCustomer.Address.Location.PostalCode = updatedCustomer.Address.Location.PostalCode;
                existingCustomer.Address.Location.Country = updatedCustomer.Address.Location.Country;

                var result = await _customerRepository.UpdateAsync(existingCustomer);
                return result
                    ? new ServiceResponse<CustomerDTO>(CustomerFactory.ToDTO(existingCustomer), true, "Customer updated successfully.")
                    : new ServiceResponse<CustomerDTO>(null!, false, "Failed to update customer.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<CustomerDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCustomerAsync(int customerId)
        {
            try
            {
                if (customerId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid customer ID.");

                var existingCustomer = await _customerRepository.GetAsync(c => c.Id == customerId);
                if (existingCustomer == null)
                    return new ServiceResponse<bool>(false, false, "Customer not found.");

                var result = await _customerRepository.RemoveAsync(existingCustomer);
                return result
                    ? new ServiceResponse<bool>(true, true, "Customer deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete customer.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
