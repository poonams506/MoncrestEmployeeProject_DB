﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoncrestEmployeeProject.Modules.Domain
{
    public class EmployeeDetail
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        // Foreign Key to Employee
        public Guid EmployeeId { get; set; }

        // Navigation property to the Employee entity
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
