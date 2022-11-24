using System;
using System.ComponentModel.DataAnnotations;

namespace Lead.Backend.Dtos
{
    public class LeadsDto
    {
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
