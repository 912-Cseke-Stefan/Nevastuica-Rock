using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class AwardEndpoint
    {
        private static readonly string ConnectionString = "Data Source=192.168.100.43,1235;Initial Catalog=Moderation;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateAward(Award award)
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

            string sql = "INSERT INTO Award VALUES (@Id,@Type)";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", award.Id);
            command.Parameters.AddWithValue("@Type", award.AwardTypeObj.ToString());
            command.ExecuteNonQuery();
        }
        public static List<Award> ReadAwards()
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
            List<Award> awards = [];
            string sql = "SELECT * FROM Award";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Award award = new ()
                {
                    Id = reader.GetGuid(0),
                    AwardTypeObj = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)),
                };
                awards.Add(award);
            }
            return awards;
        }
        public static void UpdateAward(Award award)
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
            string sql = "UPDATE Award SET Type=@T WHERE AwardId=@Id";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", award.Id);
            command.Parameters.AddWithValue("@T", award.AwardTypeObj.ToString());
            command.ExecuteNonQuery();
        }
        public static void DeleteAward(Guid id)
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
            string sql = "DELETE FROM Award WHERE AwardId=@id";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
