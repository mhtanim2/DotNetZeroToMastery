using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Auth
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
