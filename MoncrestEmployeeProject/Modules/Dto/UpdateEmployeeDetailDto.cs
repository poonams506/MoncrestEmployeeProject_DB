using System;
using System.ComponentModel.DataAnnotations;

namespace MoncrestEmployeeProject.Modules.Dto
{
    public class UpdateEmployeeDetailDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public Guid EmployeeId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
