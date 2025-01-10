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
        public void AddGroup(GroupTable group, int userId)
        {
            crud.Insert(group);
        }

        // ...
        public void AddGroupContact(MembershipTable membership, int userId)
        {
            crud.Insert(membership);
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
        public void EditGroup(GroupTable group, int userId)
        {
            crud.UpdateById(group);
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
        public IEnumerable<ContactTable> GetFavoriteContacts(int userId)
        {
            try
            {
                return conn.Query<ContactTable>("SELECT * FROM dbo.Contact C JOIN dbo.Favorite F ON C.Id = F.ContactId WHERE C.UserId = @UserId", new { UserId = userId });
            }
            catch (Exception)
            {
                return Enumerable.Empty<ContactTable>();
            }
        }

        // ...
        public IEnumerable<GroupTable> GetGroups(int userId)
        {
            try
            {
                return conn.Query<GroupTable>("SELECT * FROM dbo.Group WHERE UserId = @UserId", new { UserId = userId });
            }
            catch (Exception)
            {
                return Enumerable.Empty<GroupTable>();
            }
        }

        // ...
        public IEnumerable<ContactTable> GetGroupContacts(int groupId)
        {
            try
            {
                return conn.Query<ContactTable>("SELECT * FROM dbo.Contact C LEFT JOIN dbo.Membership M ON C.Id = M.ContactId WHERE M.GroupId = @GroupId", new { GroupId = groupId });
            }
            catch (Exception)
            {
                return Enumerable.Empty<ContactTable>();
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

        // ...
        public void RemoveGroup(int groupId, int userId)
        {
            using (var atomicTransaction = conn.BeginTransaction())
            {
                try
                {
                    conn.Execute("DELETE FROM dbo.Membership WHERE GroupId = @GroupId", new { GroupId = groupId });
                    conn.Execute("DELETE FROM dbo.Group WHERE GroupId = @GroupId", new { GroupId = groupId });

                    atomicTransaction.Commit();
                }
                catch (Exception)
                {
                    atomicTransaction.Rollback();
                }
            }
        }

        // ...
        public void RemoveGroupContact(int contactId, int userId)
        {
            conn.Execute("DELETE FROM dbo.Membership WHERE ContactId = @ContactId", new { ContactId = contactId });
        }

        // ...
    }
}
