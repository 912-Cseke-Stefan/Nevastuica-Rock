using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class JoinRequestEndpoints
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly Dictionary<Guid, JoinRequest> HardcodedJoinRequests = new()
        {
            {
                Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                // Norby's request to join Victor's study group
                new JoinRequest(Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    Guid.Parse("4017CB13-22B0-43B7-A111-50154C62CC6C"))
            }
        };
        public static void CreateJoinRequest(JoinRequest joinRequest)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                HardcodedJoinRequests.Add(joinRequest.Id, joinRequest);
                return;
            }
            using SqlConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                HardcodedJoinRequests.Add(joinRequest.Id, joinRequest);
                return;
            }

            string insertJoinRequestSql = "INSERT INTO JoinRequest (Id, UserId) VALUES (@Id, @UserId)";
            using SqlCommand command = new(insertJoinRequestSql, connection);
            command.Parameters.AddWithValue("@Id", joinRequest.Id);
            command.Parameters.AddWithValue("@UserId", joinRequest.UserId);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequest> ReadAllJoinRequests()
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. HardcodedJoinRequests.Values];
            }
            using SqlConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                return [.. HardcodedJoinRequests.Values];
            }
            List<JoinRequest> joinRequests = [];
            string sql = "SELECT Junior.Id, Junior.UserId " +
                         "FROM JoinRequest Junior ";
            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                JoinRequest joinRequest = new(
                    reader.GetGuid(0),
                    reader.GetGuid(1)
                );
                joinRequests.Add(joinRequest);
            }
            return joinRequests;
        }

        public static void DeleteJoinRequest(Guid joinRequestId)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                HardcodedJoinRequests.Remove(joinRequestId);
                return;
            }
            using SqlConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                HardcodedJoinRequests.Remove(joinRequestId);
                return;
            }
            string deleteJoinRequestSql = "DELETE FROM JoinRequest WHERE Id = @Id";
            using SqlCommand command = new(deleteJoinRequestSql, connection);
            command.Parameters.AddWithValue("@Id", joinRequestId);
            command.ExecuteNonQuery();
        }
    }
}
