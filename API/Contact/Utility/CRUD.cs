using Microsoft.Data.SqlClient;
using Dapper;
using System.Reflection;

namespace Contact.Utility
{
    // Handle non-existant instances
    public class CRUD
    {
        private SqlConnection conn;

        public CRUD(SqlConnection conn)
        {
            this.conn = conn;
        }

        //
        public IEnumerable<T> Select<T>()
        {
            Type type = typeof(T);

            string table = type.Name.Replace("Table", "");

            string query = $"SELECT * FROM [dbo].[{table}]";

            return this.conn.Query<T>(query);
        }

        //
        public T GetById<T>(int instanceId)
        {
            Type type = typeof(T);

            string table = type.Name.Replace("Table", "");

            string query = $"SELECT * FROM [dbo].[{table}] WHERE Id = @Id";

            return this.conn.QuerySingle<T>(query, new { Id = instanceId });
        }

        //
        public bool DeleteById<T>(int instanceId)
        {
            Type type = typeof(T);

            string table = type.Name.Replace("Table", "");

            string query = $"DELETE FROM [dbo].[{table}] WHERE Id = @Id";

            return this.conn.Execute(query, new { Id = instanceId }) > 0;
        }

        //
        public int Insert<T>(T AddModel)
        {
            Type type = typeof(T);

            string table = type.Name.Replace("Table", "");

            PropertyInfo[] properties = type.GetProperties();

            List<string> fields = new();
            List<string> parameters = new();

            string output = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    output = "OUTPUT inserted.ID";
                    continue;
                }

                fields.Add($"[{property.Name}]");
                parameters.Add($"@{property.Name}");
            }

            string csvFields = string.Join(", ", fields);
            string csvParams = string.Join(", ", parameters);

            string query = $"INSERT INTO [dbo].[{table}] ({csvFields}) {output} VALUES ({csvParams})";

            return this.conn.ExecuteScalar<int>(query, AddModel);
        }

        //
        public bool UpdateById<T>(T UpdateModel)
        {
            Type type = typeof(T);

            string table = type.Name.Replace("Table", "");

            PropertyInfo[] properties = type.GetProperties();

            List<string> equals = new();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                equals.Add($"[{property.Name}] = @{property.Name}");
            }

            string csvEquals = string.Join(", ", equals);

            string query = $"UPDATE [dbo].[{table}] SET {csvEquals} WHERE Id = @Id";

            int rowsAffected = this.conn.Execute(query, UpdateModel);

            return rowsAffected > 0;

            //return this.conn.ExecuteScalar<int>(query, UpdateModel) > 0;
        }
    }
}