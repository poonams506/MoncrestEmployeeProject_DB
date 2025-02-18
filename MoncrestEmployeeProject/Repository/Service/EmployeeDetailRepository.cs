using Microsoft.EntityFrameworkCore;
using MoncrestEmployeeProject.Data;
using MoncrestEmployeeProject.Modules.Domain;
using MoncrestEmployeeProject.Modules.Dto;
using MoncrestEmployeeProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoncrestEmployeeProject.Repository.Service
{
    public class EmployeeDetailRepository : IEmployeeDetails
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all employee details
        public async Task<IEnumerable<EmployeeDetailsDto>> GetAllEmployeeDetailsAsync()
        {
            return await _context.EmployeeDetails
                .Where(ed => !ed.IsDeleted)
                .Select(ed => new EmployeeDetailsDto
                {
                    Id = ed.Id,
                    Name = ed.Name,
                    Department = ed.Department
                })
                .ToListAsync();
        }

        // Get employee details by ID
        public async Task<EmployeeDetailsDto?> GetEmployeeDetailByIdAsync(Guid id)
        {
            var employeeDetail = await _context.EmployeeDetails
                .FirstOrDefaultAsync(ed => ed.Id == id && !ed.IsDeleted);

            if (employeeDetail == null)
                return null;

            return new EmployeeDetailsDto
            {
                Id = employeeDetail.Id,
                EmployeeId = employeeDetail.EmployeeId,
                Name = employeeDetail.Name,
                Department = employeeDetail.Department
            };
        }

        public async Task<EmployeeDetailsDto> CreateEmployeeDetailAsync(CreateEmployeeDetailDto employeeDetailDto)
        {
            // Check if the EmployeeId exists in the Employees table
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeDetailDto.EmployeeId);
            if (!employeeExists)
            {
                throw new ArgumentException("Invalid EmployeeId. No matching Employee found.");
            }

            var employeeDetail = new EmployeeDetail
            {
                Name = employeeDetailDto.Name,
                Department = employeeDetailDto.Department,
                EmployeeId = employeeDetailDto.EmployeeId,
                IsDeleted = employeeDetailDto.IsDeleted
            };

            _context.EmployeeDetails.Add(employeeDetail);
            await _context.SaveChangesAsync();

            return new EmployeeDetailsDto
            {
                EmployeeId = employeeDetail.EmployeeId,
                Name = employeeDetail.Name,
                Department = employeeDetail.Department
            };
        }


        // Update an existing employee detail
        public async Task<EmployeeDetailsDto?> UpdateEmployeeDetailAsync(UpdateEmployeeDetailDto employeeDetailDto)
        {
            var employeeDetail = await _context.EmployeeDetails.FindAsync(employeeDetailDto.Id);
            if (employeeDetail == null || employeeDetail.IsDeleted)
                return null;

            employeeDetail.EmployeeId = employeeDetailDto.EmployeeId;
            employeeDetail.Name = employeeDetailDto.Name;
            employeeDetail.Department = employeeDetailDto.Department;

            _context.EmployeeDetails.Update(employeeDetail);
            await _context.SaveChangesAsync();

            return new EmployeeDetailsDto
            {
                Id = employeeDetail.Id,
                Name = employeeDetail.Name,
                Department = employeeDetail.Department
            };
        }

        // Soft delete employee detail
        public async Task<bool> DeleteEmployeeDetailAsync(Guid id)
        {
            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail == null)
                return false;

            employeeDetail.IsDeleted = true;
            _context.EmployeeDetails.Update(employeeDetail);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
