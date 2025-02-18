using MoncrestEmployeeProject.Modules.Domain;
using MoncrestEmployeeProject.Data;
using MoncrestEmployeeProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoncrestEmployeeProject.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all employees with their associated network IPs
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.EmployeeNetworkIpList) // Include EmployeeNetworkIpList
                .Where(e => !e.IsDeleted) // Optional: Filter out deleted employees
                .ToListAsync();
        }

        // Get a specific employee by ID, including their network IPs
        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees
                .Include(e => e.EmployeeNetworkIpList) // Include EmployeeNetworkIpList
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted); // Optional: Check if employee is not deleted
        }

        // Create a new employee
        public async Task<Employee?> CreateEmployeeAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        // Update an existing employee
        public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees
                .Include(e => e.EmployeeNetworkIpList) // Include EmployeeNetworkIpList
                .FirstOrDefaultAsync(e => e.Id == employee.Id && !e.IsDeleted); // Optional: Ensure employee is not deleted

            if (existingEmployee == null)
                return null;

            // Update basic employee details
            existingEmployee.EmployCode = employee.EmployCode;
            existingEmployee.Name = employee.Name;
            existingEmployee.DOB = employee.DOB;
            existingEmployee.Email = employee.Email;
            existingEmployee.Mobile = employee.Mobile;
            existingEmployee.State = employee.State;
            existingEmployee.City = employee.City;

            // Clear the existing network IPs and add new ones
            if (employee.EmployeeNetworkIpList != null)
            {
                var existingIps = employee.EmployeeNetworkIpList.ToList();

                foreach (var ip in existingIps)
                {
                    if (!employee.EmployeeNetworkIpList.Any(x => x.Id == ip.Id))
                    {
                        _context.EmployeeNetworkIpss.Remove(ip);
                    }
                }

                // Add new IP addresses or update existing ones
                foreach (var ip in employee.EmployeeNetworkIpList)
                {
                    if (ip.Id == Guid.Empty) // New IP address
                    {
                        ip.EmployeeId = employee.Id;
                        await _context.EmployeeNetworkIpss.AddAsync(ip);
                    }
                    else
                    {
                        // Update existing IP address if necessary (optional)
                        var existingIp = employee.EmployeeNetworkIpList
                            .FirstOrDefault(x => x.Id == ip.Id);
                        if (existingIp != null)
                        {
                            existingIp.IpAddress = ip.IpAddress;
                        }
                    }
                }
            }

            // Save changes to the database
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        // Soft delete an employee by ID
        public async Task<Employee?> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.IsDeleted = true; // Mark employee as deleted
            _context.Employees.Update(employee);

            // Also mark associated network IPs as deleted (soft delete)
            var networkIps = _context.EmployeeNetworkIpss.Where(ip => ip.EmployeeId == id).ToList();
            foreach (var networkIp in networkIps)
            {
                networkIp.IsDeleted = true;
                _context.EmployeeNetworkIpss.Update(networkIp);
            }

            await _context.SaveChangesAsync();
            return employee;
        }

        // Get list of network IPs for a specific employee
        public async Task<IEnumerable<EmployeeNetworkIps>> GetEmployeeNetworkIpsAsync(Guid employeeId)
        {
            return await _context.EmployeeNetworkIpss
                .Where(ip => ip.EmployeeId == employeeId && !ip.IsDeleted) // Optional: Exclude deleted network IPs
                .ToListAsync();
        }
    }
}
