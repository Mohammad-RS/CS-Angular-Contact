using System.ComponentModel.DataAnnotations;

namespace Contact.Model.Phone
{
    public class PhoneTable
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public byte PhoneTypeId { get; set; }

        [StringLength(20)]
        public string Number { get; set; }
    }
}
