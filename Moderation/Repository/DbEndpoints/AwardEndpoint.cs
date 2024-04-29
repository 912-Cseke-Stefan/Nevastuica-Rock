using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;

namespace Moderation.DbEndpoints
{
    public class AwardEndpoint
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly Dictionary<Guid, Award> HardcodedAwards = [];
        public static void CreateAward(Award award)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                HardcodedAwards.Add(award.Id, award);
                return;
            }
            using SqlConnection connection = new (connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. HardcodedAwards.Values];
            }
            using SqlConnection connection = new (connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!HardcodedAwards.ContainsKey(award.Id))
                {
                    return;
                }

                HardcodedAwards[award.Id] = award;
                return;
            }
            using SqlConnection connection = new (connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                HardcodedAwards.Remove(id);
                return;
            }
            using SqlConnection connection = new (connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
