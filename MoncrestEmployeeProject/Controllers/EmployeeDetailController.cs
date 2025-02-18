using Microsoft.AspNetCore.Mvc;
using MoncrestEmployeeProject.Modules.Dto;
using MoncrestEmployeeProject.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoncrestEmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailController : ControllerBase
    {
        private readonly IEmployeeDetails _employeeDetailRepository;

        // Inject the repository interface
        public EmployeeDetailController(IEmployeeDetails employeeDetailRepository)
        {
            _employeeDetailRepository = employeeDetailRepository;
        }

        // GET api/employeedetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDetailsDto>>> GetEmployeeDetails()
        {
            var employeeDetails = await _employeeDetailRepository.GetAllEmployeeDetailsAsync();
            return Ok(employeeDetails);
        }

        // GET api/employeedetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailsDto>> GetEmployeeDetail(Guid id)
        {
            var employeeDetail = await _employeeDetailRepository.GetEmployeeDetailByIdAsync(id);

            if (employeeDetail == null)
                return NotFound();

            return Ok(employeeDetail);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDetailsDto>> CreateEmployeeDetail(CreateEmployeeDetailDto createEmployeeDetailDto)
        {
            var employeeDetail = await _employeeDetailRepository.CreateEmployeeDetailAsync(createEmployeeDetailDto);
            return CreatedAtAction(nameof(GetEmployeeDetail), new { id = employeeDetail.Id }, employeeDetail);
        }


        // PUT api/employeedetail
        [HttpPut]
        public async Task<ActionResult<EmployeeDetailsDto>> UpdateEmployeeDetail(UpdateEmployeeDetailDto updateEmployeeDetailDto)
        {
            var employeeDetail = await _employeeDetailRepository.UpdateEmployeeDetailAsync(updateEmployeeDetailDto);

            if (employeeDetail == null)
                return NotFound();

            return Ok(employeeDetail);
        }

        // DELETE api/employeedetail/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeDetail(Guid id)
        {
            var success = await _employeeDetailRepository.DeleteEmployeeDetailAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
