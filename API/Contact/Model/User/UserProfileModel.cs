using System.ComponentModel.DataAnnotations;

namespace Contact.Model.User
{
    public class UserProfileModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        public string Avatar { get; set; }

        public DateTime Date_Created { get; set; }

        public bool Is_Verified { get; set; }
    }
}
