using System.ComponentModel.DataAnnotations;

namespace Contact.Model.Phone
{
    public class PhoneTypeTable
    {
        public byte Id { get; set; }

        [StringLength(20)]
        public string Title { get; set; }
    }
}
