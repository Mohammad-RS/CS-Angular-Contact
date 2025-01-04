using System.ComponentModel.DataAnnotations;

namespace Contact.Model.Contact
{
    public class ContactTable
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        [StringLength(100)]
        public string Avatar { get; set; }

        public DateOnly Date_Of_Birth { get; set; }

        public DateTime Date_Created { get; set; }

        public DateTime Date_Modified { get; set; }
    }
}
