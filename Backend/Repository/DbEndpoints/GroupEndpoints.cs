using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;

namespace Moderation.DbEndpoints
{
    internal class GroupEndpoints
    {
        private static readonly string ConnectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static readonly Dictionary<Guid, Group> HardcodedGroups = new ()
        {
            {
                Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),
                new Group (
                    Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),
                    "Izabella's birthday party",
                    "balabla",
                    new User("Izabella"))
            },
            {
                Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                new Group (
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                    "Victor's study group",
                    "none provided",
                    new User("Victor"))
            }
        };
        public static void CreateGroup(Group group)
        {
            if (!ApplicationState.DbConnectionIsAvailable)
            {
                HardcodedGroups.Add(group.Id, group);
                return;
            }
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.DbConnectionIsAvailable = false;
                HardcodedGroups.Add(group.Id, group);
                return;
            }

            string sql = "INSERT INTO [Group] (Id, Name, Description, Owner) " +
                         "VALUES (@Id, @Name, @Description, @Owner)";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", group.Id);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Owner", group.Creator.Id);

            command.ExecuteNonQuery();
        }
        public static List<Group> ReadAllGroups()
        {
            if (!ApplicationState.DbConnectionIsAvailable)
            {
                return [.. HardcodedGroups.Values];
            }

            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.DbConnectionIsAvailable = false;
                return [.. HardcodedGroups.Values];
            }

            List<Group> groups = [];
            string sql = "SELECT Id, Name, Description, Owner FROM [Group]";

            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var userId = reader.GetGuid(3);
                string username = ApplicationState.Get()
                    .UserRepository?.Get(userId)?
                    .Username // if anything is null along the way throw an exception:
                    ?? throw new Exception("No username by that id");
                User user = new (userId, username);

                Group group = new (reader.GetGuid(0), reader.GetString(1), reader.GetString(2), user);
                groups.Add(group);
            }
            return groups;
        }
        private static void UpdateGroupIfDBUnvailable(Group group)
        {
            if (HardcodedGroups[group.Id] == null)
            {
                return;
            }

            HardcodedGroups[group.Id] = group;
            return;
        }
        public static void UpdateGroup(Group group)
        {
            if (!ApplicationState.DbConnectionIsAvailable)
            {
                UpdateGroupIfDBUnvailable(group);
                return;
            }
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.DbConnectionIsAvailable = false;
                UpdateGroupIfDBUnvailable(group);
                return;
            }
            string sql = "UPDATE Group" +
                         "SET Name = @Name, Description = @Description, Owner = @Owner" +
                         "WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Owner", group.Creator.Id);

            command.ExecuteNonQuery();
        }
        public static void DeleteGroup(Guid id)
        {
            if (!ApplicationState.DbConnectionIsAvailable)
            {
                HardcodedGroups.Remove(id);
                return;
            }
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.DbConnectionIsAvailable = false;
                HardcodedGroups.Remove(id);
                return;
            }

            string sql = "DELETE FROM Group WHERE Id = @Id";

            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}
