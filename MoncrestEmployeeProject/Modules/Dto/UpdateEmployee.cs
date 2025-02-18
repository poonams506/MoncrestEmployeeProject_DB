using MoncrestEmployeeProject.Modules.Domain;
using System.ComponentModel.DataAnnotations;

namespace MoncrestEmployeeProject.Modules.Dto
{
    public class UpdateEmployee
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

        public bool IsDeleted { get; set; } = false;

        // Adding State and City properties
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public List<UpdateEmployeeNetworkIpsDto> EmployeeNetworkIps { get; set; } = new List<UpdateEmployeeNetworkIpsDto>();


    }
}
