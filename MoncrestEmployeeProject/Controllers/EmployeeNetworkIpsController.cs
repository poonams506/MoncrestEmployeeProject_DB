using Microsoft.AspNetCore.Mvc;
using MoncrestEmployeeProject.Modules.Domain;
using MoncrestEmployeeProject.Modules.Dto;
using MoncrestEmployeeProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoncrestEmployeeProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeNetworkIpController : ControllerBase
    {
        private readonly IEmployeeNetworkIps _employeeNetworkIpsRepository;

        public EmployeeNetworkIpController(IEmployeeNetworkIps employeeNetworkIpsRepository)
        {
            _employeeNetworkIpsRepository = employeeNetworkIpsRepository;
        }

        // Create a new EmployeeNetworkIps record using DTO
        [HttpPost]
        public async Task<ActionResult> CreateEmployeeNetworkIpAsync([FromBody] CreateEmployeeNetworkIpsDto createEmployeeNetworkIpsDto)
        {
            if (createEmployeeNetworkIpsDto == null)
            {
                return BadRequest("Invalid data.");
            }

            // Map DTO to Domain Model
            var networkIp = new EmployeeNetworkIps
            {
                Id = Guid.NewGuid(), // Generating a new Guid for Id
                IpAddress = createEmployeeNetworkIpsDto.IpAddress,
                EmployeeId = Guid.NewGuid() // Assign EmployeeProfileId as needed
            };

            var createdNetworkIp = await _employeeNetworkIpsRepository.CreateEmployeeNetworkIpAsync(networkIp);
            return CreatedAtAction(nameof(GetEmployeeNetworkIpByIdAsync), new { id = createdNetworkIp.Id }, createdNetworkIp);
        }

        // Get an EmployeeNetworkIps record by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeNetworkIps>> GetEmployeeNetworkIpByIdAsync(Guid id)
        {
            var networkIp = await _employeeNetworkIpsRepository.GetEmployeeNetworkIpByIdAsync(id);
            if (networkIp == null)
            {
                return NotFound();
            }
            return Ok(networkIp);
        }

        // Get all EmployeeNetworkIps records by EmployeeProfileId
        [HttpGet("byEmployee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeNetworkIps>>> GetEmployeeNetworkIpsByEmployeeIdAsync(Guid employeeId)
        {
            var networkIps = await _employeeNetworkIpsRepository.GetEmployeeNetworkIpsByEmployeeIdAsync(employeeId);
            return Ok(networkIps);
        }

        // Update an existing EmployeeNetworkIps record
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeNetworkIps>> UpdateEmployeeNetworkIpAsync(Guid id, [FromBody] EmployeeNetworkIps networkIp)
        {
            if (id != networkIp.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var updatedNetworkIp = await _employeeNetworkIpsRepository.UpdateEmployeeNetworkIpAsync(networkIp);
            if (updatedNetworkIp == null)
            {
                return NotFound();
            }

            return Ok(updatedNetworkIp);
        }

        // Delete an EmployeeNetworkIps record by Id
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeNetworkIps>> DeleteEmployeeNetworkIpAsync(Guid id)
        {
            var networkIp = await _employeeNetworkIpsRepository.DeleteEmployeeNetworkIpAsync(id);
            if (networkIp == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
