using Contact.Model.User;
using Contact.Utility;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Contact.Data
{
    public class UserData
    {
        private string connectionString = "Data Source=.;Database=Contact;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private SqlConnection conn;

        private CRUD crud;

        public UserData()
        {
            this.conn = new SqlConnection(connectionString);

            this.crud = new CRUD(conn);
        }

        // ...
        public int Insert(UserTable user)
        {
            try
            {
                return crud.Insert(user);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // ...
        public int GetUserId(string username, byte[] password)
        {
            string query = "SELECT Id FROM dbo.[User] WHERE Username = @Username AND Password = @Password";

            try
            {
                return conn.ExecuteScalar<int>(query, new { Username = username, Password = password });
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // ...
        public UserTable GetUser(int userId)
        {
            try
            {
                return crud.GetById<UserTable>(userId);
            }
            catch (Exception)
            {
                return new UserTable();
            }
        }

        // ...
        public UserProfileModel GetUserProfileById(int userId)
        {
            try
            {
                UserTable user = crud.GetById<UserTable>(userId);

                UserProfileModel profile = new UserProfileModel()
                {
                    Username = user.Username,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Avatar = user.Avatar,
                    Date_Created = user.Date_Created,
                    Is_Verified = user.Is_Verified
                };

                return profile;
            }
            catch (Exception)
            {
                return new UserProfileModel();
            }
        }

        // ...
        public VisitUserProfileModel GetUserProfileByUsername(string username)
        {
            string query = "SELECT Id FROM dbo.[User] WHERE Username = @Username";

            try
            {
                int Id = conn.ExecuteScalar<int>(query, new { Username = username });

                UserTable user = crud.GetById<UserTable>(Id);

                VisitUserProfileModel profile = new VisitUserProfileModel()
                {
                    Username = user.Username,
                    Fullname = user.Fullname,
                    Avatar = user.Avatar,
                    Date_Created = user.Date_Created
                };

                return profile;
            }
            catch (Exception)
            {
                return new VisitUserProfileModel();
            }
        }

        // ...
        public bool CheckUsernameAndEmailUniqueness(string username, string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM dbo.[User] WHERE Username = @Username OR Email = @Email";

                int count = conn.ExecuteScalar<int>(query, new { Username = username, Email = email });

                return count == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ...
        public bool Update(UserTable user)
        {
            try
            {
                return crud.UpdateById(user);
            }
            catch (Exception) 
            { 
                return false;
            }
        }

        // ...
        public bool Delete(int Id)
        {
            try
            {
                return crud.DeleteById<UserTable>(Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ...
    }
}
