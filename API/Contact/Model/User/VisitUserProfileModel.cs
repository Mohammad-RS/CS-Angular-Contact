using System.ComponentModel.DataAnnotations;

namespace Contact.Model.User
{
    public class VisitUserProfileModel
    {
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        public string Avatar { get; set; }

        public DateTime Date_Created { get; set; }
    }
}