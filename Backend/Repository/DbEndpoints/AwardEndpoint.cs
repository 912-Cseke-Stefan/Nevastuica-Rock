using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;

namespace Moderation.DbEndpoints
{
    public class AwardEndpoint
    {
        private static readonly string ConnectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static readonly Dictionary<Guid, Award> HardcodedAwards = [];
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
                HardcodedAwards.Add(award.Id, award);
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
                return [.. HardcodedAwards.Values];
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
                if (!HardcodedAwards.ContainsKey(award.Id))
                {
                    return;
                }

                HardcodedAwards[award.Id] = award;
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
                HardcodedAwards.Remove(id);
                return;
            }
            string sql = "DELETE FROM Award WHERE AwardId=@id";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
