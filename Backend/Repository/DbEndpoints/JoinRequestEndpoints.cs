using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class JoinRequestEndpoints
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateJoinRequest(JoinRequest joinRequest)
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

            string insertJoinRequestSql = "INSERT INTO JoinRequest (Id, UserId) VALUES (@Id, @UserId)";
            using SqlCommand command = new (insertJoinRequestSql, connection);
            command.Parameters.AddWithValue("@Id", joinRequest.Id);
            command.Parameters.AddWithValue("@UserId", joinRequest.UserId);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequest> ReadAllJoinRequests()
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return [];
            }
            List<JoinRequest> joinRequests = [];
            string sql = "SELECT Junior.Id, Junior.UserId " +
                         "FROM JoinRequest Junior ";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                JoinRequest joinRequest = new (
                    reader.GetGuid(0),
                    reader.GetGuid(1));
                joinRequests.Add(joinRequest);
            }
            return joinRequests;
        }

        public static void DeleteJoinRequest(Guid joinRequestId)
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
            string deleteJoinRequestSql = "DELETE FROM JoinRequest WHERE Id = @Id";
            using SqlCommand command = new (deleteJoinRequestSql, connection);
            command.Parameters.AddWithValue("@Id", joinRequestId);
            command.ExecuteNonQuery();
        }
    }
}
