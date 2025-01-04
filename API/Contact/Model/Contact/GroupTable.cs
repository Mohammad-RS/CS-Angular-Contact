using System.ComponentModel.DataAnnotations;

namespace Contact.Model.Contact
{
    public class GroupTable
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Avatar { get; set; }
    }
}
