using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Model;

namespace Moderation.DbEndpoints
{
    public class ReportEndpoint
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreatePostReport(PostReport postReport)
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

            string sql = "INSERT INTO Report (ReportId, UserId, PostId, Message, GroupId) " +
                         "VALUES (@ReportId, @UserId, @PostId, @Message, @GroupId)";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@ReportId", postReport.Id);
            command.Parameters.AddWithValue("@UserId", postReport.UserId);
            command.Parameters.AddWithValue("@PostId", postReport.PostId);
            command.Parameters.AddWithValue("@Message", postReport.Message);
            command.Parameters.AddWithValue("@GroupId", postReport.GroupId);
            command.ExecuteNonQuery();
        }

        public static List<PostReport> ReadAllPostReports()
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
            List<PostReport> postReports = [];

            string sql = "SELECT ReportId, UserId, PostId, Message, GroupId FROM Report";

            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PostReport postReport = new (reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetString(3), reader.GetGuid(4));
                postReports.Add(postReport);
            }

            return postReports;
        }

        public static void DeletePostReport(Guid reportId)
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

            string sql = "DELETE FROM Report WHERE ReportId = @ReportId";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@ReportId", reportId);

            command.ExecuteNonQuery();
        }

        public static void UpdatePostReport(Guid id, PostReport postReport)
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
            string sqlCommandString = "UPDATE Report" +
                                      $"SET UserId = {postReport.UserId}," +
                                      $"PostId = {postReport.PostId}," +
                                      $"Message = {postReport.Message}," +
                                      $"GroupId = {postReport.GroupId}" +
                                      $"WHERE ReportId = {id}";

            using SqlCommand command = new (sqlCommandString, connection);

            command.ExecuteNonQuery();
        }
    }
}