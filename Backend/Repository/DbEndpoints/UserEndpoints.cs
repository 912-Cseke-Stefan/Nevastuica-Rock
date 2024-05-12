using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";

        public static void CreateUser(User user)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            string sql = "INSERT INTO [User] (Id, Username, Password) " +
                         "VALUES (@Id, @Username, @Password)";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();
        }

        public static List<User> ReadAllUsers()
        {
            List<User> users = [];
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return new List<User>();
            }
            string sql = "SELECT Id, Username, Password FROM [User]";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                User user = new (reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                users.Add(user);
            }
            return users;
        }
        public static void UpdateUser(User newValues)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }
            string sql = "UPDATE User" +
                         "SET Username = @Username, Password = @Password" +
                         "WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Username", newValues.Username);
            command.Parameters.AddWithValue("@Password", newValues.Password);
            command.Parameters.AddWithValue("@Id", newValues.Id);

            command.ExecuteNonQuery();
        }
        public static void DeleteUser(Guid id)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return;
            }

            string sql = "DELETE FROM User WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}