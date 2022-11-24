using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Leads
    {
        public Guid Id { get; set; }

        public Leads()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        public string FirsName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Company { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
    }
}