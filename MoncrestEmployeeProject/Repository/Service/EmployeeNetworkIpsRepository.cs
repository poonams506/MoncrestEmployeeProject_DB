using Microsoft.EntityFrameworkCore;
using MoncrestEmployeeProject.Modules.Domain;
using MoncrestEmployeeProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoncrestEmployeeProject.Repository.Interface;

namespace MoncrestEmployeeProject.Repository
{
    public class EmployeeNetworkIpsRepository : IEmployeeNetworkIps
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeNetworkIpsRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        // Create a new EmployeeNetworkIps record
        public async Task<EmployeeNetworkIps> CreateEmployeeNetworkIpAsync(EmployeeNetworkIps networkIp)
        {
            if (networkIp == null)
                throw new ArgumentNullException(nameof(networkIp));

            await _dbContext.EmployeeNetworkIpss.AddAsync(networkIp);
            await _dbContext.SaveChangesAsync();
            return networkIp;
        }

        // Delete an EmployeeNetworkIps record by its Id
        public async Task<EmployeeNetworkIps?> DeleteEmployeeNetworkIpAsync(Guid id)
        {
            var networkIp = await _dbContext.EmployeeNetworkIpss.FirstOrDefaultAsync(ip => ip.Id == id);
            if (networkIp == null)
                return null;

            _dbContext.EmployeeNetworkIpss.Remove(networkIp);
            await _dbContext.SaveChangesAsync();
            return networkIp;
        }

        // Retrieve a specific EmployeeNetworkIps record by its Id
        public async Task<EmployeeNetworkIps?> GetEmployeeNetworkIpByIdAsync(Guid id)
        {
            return await _dbContext.EmployeeNetworkIpss.FirstOrDefaultAsync(ip => ip.Id == id);
        }

        // Retrieve all EmployeeNetworkIps records for a given EmployeeProfile by its Id
        public async Task<IEnumerable<EmployeeNetworkIps>> GetEmployeeNetworkIpsByEmployeeIdAsync(Guid employeeId)
        {
            return await _dbContext.EmployeeNetworkIpss
                                   .Where(ip => ip.EmployeeId == employeeId)
                                   .ToListAsync();
        }

        // Update an existing EmployeeNetworkIps record
        public async Task<EmployeeNetworkIps> UpdateEmployeeNetworkIpAsync(EmployeeNetworkIps networkIp)
        {
            if (networkIp == null)
                throw new ArgumentNullException(nameof(networkIp));

            var existingIp = await _dbContext.EmployeeNetworkIpss.FirstOrDefaultAsync(ip => ip.Id == networkIp.Id);
            if (existingIp == null)
                throw new KeyNotFoundException($"EmployeeNetworkIps with id {networkIp.Id} not found.");

            // Update properties (for example, updating IpAddress)
            existingIp.IpAddress = networkIp.IpAddress;
            // If needed, update additional properties here (e.g., EmployeeProfileId)

            _dbContext.EmployeeNetworkIpss.Update(existingIp);
            await _dbContext.SaveChangesAsync();
            return existingIp;
        }
    }
}
