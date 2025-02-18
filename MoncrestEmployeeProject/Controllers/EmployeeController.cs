using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoncrestEmployeeProject.Modules.Domain;
using MoncrestEmployeeProject.Modules.Dto;
using MoncrestEmployeeProject.Repository.Interface;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployee _employeeRepository;

    public EmployeeController(IEmployee employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    // GET: api/Employee
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        if (employees == null || !employees.Any())
        {
            return NotFound("No employees found.");
        }

        var employeeDtos = employees.Select(emp => new EmployeeDto
        {
            Id = emp.Id,
            EmployCode = emp.EmployCode,
            Name = emp.Name,
            Mobile = emp.Mobile,
            DOB = emp.DOB,
            Email = emp.Email,
            State = emp.State,
            City = emp.City
        }).ToList();

        return Ok(employeeDtos);
    }

    // GET: api/Employee/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound($"Employee with ID {id} not found.");
        }

        // Return employee data as DTO
        var employeeDto = new EmployeeDto
        {
            Id = employee.Id,
            EmployCode = employee.EmployCode,
            Name = employee.Name,
            Mobile = employee.Mobile,
            DOB = employee.DOB,
            Email = employee.Email,
            State = employee.State,
            City = employee.City
        };

        return Ok(employeeDto);
    }

    // POST: api/Employee/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeDto obj)
    {
        if (obj == null || obj.EmployeeNetworkIps == null)
        {
            return BadRequest("Invalid input data.");
        }

        var employee = new Employee
        {
            EmployCode = obj.EmployCode,
            Name = obj.Name,
            Mobile = obj.Mobile,
            DOB = obj.DOB,
            Email = obj.Email,
            State = obj.State,
            City = obj.City,
            EmployeeNetworkIpList = obj.EmployeeNetworkIps
                .Select(ipDto => new EmployeeNetworkIps { IpAddress = ipDto.IpAddress })
                .ToList()
        };

        var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = createdEmployee.Id }, createdEmployee);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeAsync(Guid id, [FromBody] UpdateEmployee obj)
    {
        if (obj == null || obj.EmployeeNetworkIps == null)
        {
            return BadRequest("Invalid input data.");
        }

        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (existingEmployee == null)
        {
            return NotFound($"Employee with ID {id} not found.");
        }

        existingEmployee.EmployCode = obj.EmployCode;
        existingEmployee.Name = obj.Name;
        existingEmployee.DOB = obj.DOB;
        existingEmployee.Mobile = obj.Mobile;
        existingEmployee.Email = obj.Email;
        existingEmployee.State = obj.State;
        existingEmployee.City = obj.City;

        // Clear old network IPs and add new ones
        existingEmployee.EmployeeNetworkIpList.Clear();
        foreach (var ipDto in obj.EmployeeNetworkIps)
        {
            existingEmployee.EmployeeNetworkIpList.Add(new EmployeeNetworkIps { IpAddress = ipDto.IpAddress });
        }

        var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
        return Ok(updatedEmployee);
    }


    // DELETE: api/Employee/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
    {
        var deletedEmployee = await _employeeRepository.DeleteEmployeeAsync(id);
        if (deletedEmployee == null)
        {
            return NotFound($"Employee with ID {id} not found.");
        }
        return Ok($"Employee with ID {id} has been deleted.");
    }

    // Get all network IPs for a specific employee
    [HttpGet("network-ips/{employeeId}")]
    public async Task<ActionResult<IEnumerable<EmployeeNetworkIps>>> GetEmployeeNetworkIpsAsync(Guid employeeId)
    {
        var networkIps = await _employeeRepository.GetEmployeeNetworkIpsAsync(employeeId); // Call the method directly

        if (networkIps == null || !networkIps.Any())
        {
            return NotFound("No network IPs found for the specified employee.");
        }

        return Ok(networkIps);
    }

}
