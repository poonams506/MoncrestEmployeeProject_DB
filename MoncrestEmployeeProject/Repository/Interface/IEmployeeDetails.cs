using MoncrestEmployeeProject.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoncrestEmployeeProject.Repository.Interface
{
    public interface IEmployeeDetails
    {
        // Fetch all employee details
        Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeDetailsAsync();

        // Fetch employee details by ID
        Task<EmployeeDetailsDto?> GetEmployeeDetailByIdAsync(Guid id);

        // Create a new employee detail
        Task<EmployeeDetailsDto> CreateEmployeeDetailAsync(CreateEmployeeDetailDto employeeDetail);

        // Update an existing employee detail
        Task<EmployeeDetailsDto?> UpdateEmployeeDetailAsync(UpdateEmployeeDetailDto employeeDetail);

        // Soft delete an employee detail
        Task<bool> DeleteEmployeeDetailAsync(Guid id);


    }
}
