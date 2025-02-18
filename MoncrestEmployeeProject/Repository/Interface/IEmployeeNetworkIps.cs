using MoncrestEmployeeProject.Modules.Domain;

namespace MoncrestEmployeeProject.Repository.Interface
{
    public interface IEmployeeNetworkIps
    {
        // Create a new EmployeeNetworkIps record
        Task<EmployeeNetworkIps> CreateEmployeeNetworkIpAsync(EmployeeNetworkIps networkIp);

        // Retrieve all EmployeeNetworkIps records for a given EmployeeProfile by its Id
        Task<IEnumerable<EmployeeNetworkIps>> GetEmployeeNetworkIpsByEmployeeIdAsync(Guid employeeId);

        // Retrieve a specific EmployeeNetworkIps record by its Id
        Task<EmployeeNetworkIps?> GetEmployeeNetworkIpByIdAsync(Guid id);

        // Update an existing EmployeeNetworkIps record
        Task<EmployeeNetworkIps> UpdateEmployeeNetworkIpAsync(EmployeeNetworkIps networkIp);

        // Delete an EmployeeNetworkIps record by its Id
        Task<EmployeeNetworkIps?> DeleteEmployeeNetworkIpAsync(Guid id);
    }
}
