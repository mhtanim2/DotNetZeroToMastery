using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.Dto.Request
{
    public class LogInRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
