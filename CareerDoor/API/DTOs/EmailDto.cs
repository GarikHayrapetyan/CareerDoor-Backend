using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class EmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
