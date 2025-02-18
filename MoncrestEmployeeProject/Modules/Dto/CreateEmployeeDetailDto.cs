using System.ComponentModel.DataAnnotations;

namespace MoncrestEmployeeProject.Modules.Dto
{
    public class CreateEmployeeDetailDto
    {
        public string Name { get; set; }       // Add Name property
        public string Department { get; set; }
        public Guid EmployeeId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
