using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoncrestEmployeeProject.Modules.Domain
{
    public class EmployeeNetworkIps
    {
        public Guid Id { get; set; }

        [Required]
        public string IpAddress { get; set; } = string.Empty;


        public Guid EmployeeId { get; set; }

        // Navigation property to the Employee entity
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
