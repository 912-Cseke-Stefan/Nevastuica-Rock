﻿using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";

        /// <summary>
        ///  Azure has a monthly free limit that we went over. The db will be once again available starting May 1st 2024, but in the meantime,
        ///  use these hardcoded values:
        /// </summary>
        private static readonly List<User> HardcodedUsers = [new User(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"), "victor", "alabala"),
                                                     new User(Guid.Parse("0825D1FD-C40B-4926-A128-2D924D564B3E"), "boti", "ababab"),
                                                     new User(Guid.Parse("E17FF7A1-95DF-4EAE-8A69-9B139CCD7CA8"), "norby", "norb"),
                                                     new User(Guid.Parse("E268B52E-DD82-4D86-AE17-9F8DE883BEFE"), "ioan", "neon"),
                                                     new User(Guid.Parse("E268B52E-DD82-4D86-AE17-9F8DE883BEFE"), "cipri", "bn"),
                                                     new User(Guid.Parse("9EBE3762-1CD6-45BD-AF9F-0D221CB078D1"), "izabella", "yup")];
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
                HardcodedUsers.Add(user);
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
                return HardcodedUsers;
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
                User? toUpdate = HardcodedUsers.Where(u => u.Id == newValues.Id).First();
                if (toUpdate == null)
                {
                    return;
                }

                toUpdate.Username = newValues.Username;
                toUpdate.Password = newValues.Password;
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
                User? toRemove = HardcodedUsers.Where(u => u.Id == id).First();
                if (toRemove == null)
                {
                    return;
                }

                HardcodedUsers.Remove(toRemove);
                return;
            }

            string sql = "DELETE FROM User WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}