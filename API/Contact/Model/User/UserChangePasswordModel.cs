using System.ComponentModel.DataAnnotations;

namespace Contact.Model.User
{
    public class UserChangePasswordModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password1 { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password2 { get; set; }
    }
}
