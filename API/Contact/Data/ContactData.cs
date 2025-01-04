using Contact.Model.Contact;
using Contact.Model.Phone;
using Contact.Utility;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Contact.Data
{
    public class ContactData
    {
        private string connectionString = "Data Source=.;Database=Contact;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private SqlConnection conn;

        private CRUD crud;

        public ContactData()
        {
            this.conn = new SqlConnection(connectionString);

            this.crud = new CRUD(conn);
        }

        // ...
        public IEnumerable<PhoneTypeTable> GetPhoneTypes()
        {
            try
            {
                return this.crud.Select<PhoneTypeTable>();
            }
            catch (Exception)
            {
                return Enumerable.Empty<PhoneTypeTable>();
            }
        }

        // ...
        public void AddContact(ContactTable contact)
        {
            crud.Insert(contact);
        }

        // ...
        public void AddPhone(PhoneTable phone, int userId)
        {
            crud.Insert(phone);
        }

        // ...
        public void AddFavorite(FavoriteTable favorite, int userId)
        {
            crud.Insert(favorite);
        }

        // ...
        public void EditContact(ContactTable contact)
        {
            crud.UpdateById(contact);
        }

        // ...
        public void EditPhone(PhoneTable phone, int userId)
        {
            crud.UpdateById(phone);
        }

        // ...
        public ContactTable GetContact(int contactId, int userId)
        {
            try
            {
                return crud.GetById<ContactTable>(contactId);
            }
            catch (Exception)
            {
                return new ContactTable();
            }

        }

        // ...
        public IEnumerable<ContactTable> GetContacts(int userId)
        {
            try
            {
                return crud.Select<ContactTable>();
            }
            catch (Exception)
            { 
                return Enumerable.Empty<ContactTable>();
            }
        }

        // ...
        public IEnumerable<PhoneTable> GetPhones(int contactId, int userId)
        {
            try
            {
                return conn.Query<PhoneTable>("SELECT * FROM dbo.Phone WHERE ContactId = @ContactId", new { ContactId = contactId });
            }
            catch (Exception)
            {
                return Enumerable.Empty<PhoneTable>();
            }
        }

        // ...
        public void RemoveContact(int contactId, int userId)
        {
            crud.DeleteById<ContactTable>(contactId);
        }

        // ...
        public void RemovePhone(int phoneId, int userId)
        {
            crud.DeleteById<PhoneTable>(phoneId);
        }

        // ...
        public void RemoveFavorite(int contactId, int userId)
        {
            conn.Execute("DELETE FROM dbo.Favorite WHERE ContactId = @ContactId", new { ContactId = contactId });
        }
    }
}
