using MoncrestEmployeeProject.Modules.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoncrestEmployeeProject.Modules.Dto
{
    public class EmployeeNetworkIpsDto
    {
        public Guid Id { get; set; }

        [Required]
        public string IpAddress { get; set; } = string.Empty;


        public Guid EmployeeId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
