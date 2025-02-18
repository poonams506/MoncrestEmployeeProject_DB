﻿using MoncrestEmployeeProject.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace MoncrestEmployeeProject.Modules.Domain
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        public int EmployCode { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Mobile { get; set; } = string.Empty;

        // Adding State and City properties
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;


        public ICollection<EmployeeNetworkIps> EmployeeNetworkIpList { get; set; }


        public bool IsDeleted { get; set; } = false;

    
    }
}
