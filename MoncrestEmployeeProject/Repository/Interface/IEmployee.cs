using MoncrestEmployeeProject.Modules.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoncrestEmployeeProject.Repository.Interface
{
    public interface IEmployee
    {
        // Get all employees, including associated IP list
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        // Get a specific employee by ID, including associated IP list
        Task<Employee?> GetEmployeeByIdAsync(Guid id);

        // Create a new employee
        Task<Employee?> CreateEmployeeAsync(Employee employee);

        // Update an existing employee
        Task<Employee?> UpdateEmployeeAsync(Employee employee);

        // Soft delete an employee by ID
        Task<Employee?> DeleteEmployeeAsync(Guid id);

        // Optional: Get list of network IPs for a specific employee
        Task<IEnumerable<EmployeeNetworkIps>> GetEmployeeNetworkIpsAsync(Guid employeeId);
    }
}
