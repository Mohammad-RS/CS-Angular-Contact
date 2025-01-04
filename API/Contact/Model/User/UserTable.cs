using System.ComponentModel.DataAnnotations;

namespace Contact.Model.User
{
    public class UserTable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        public byte[] Password { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(100)]
        public string Avatar { get; set; }

        public DateTime Date_Created { get; set; }

        public DateTime Date_Modified { get; set; }

        public bool Is_Verified { get; set; }
    }
}
