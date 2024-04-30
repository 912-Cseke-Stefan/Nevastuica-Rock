using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;
using ApplicationState = Moderation.Serivce.ApplicationState;

namespace Moderation.DbEndpoints
{
    internal class GroupEndpoints
    {
        private static readonly Dictionary<Guid, Group> HardcodedGroups = new ()
        {
            {
                Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),
                new Group (
                    Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),
                    "Izabella's birthday party",
                    "balabla",
                    ApplicationState.Get().UserRepository?.Get(Guid.Parse("9EBE3762-1CD6-45BD-AF9F-0D221CB078D1")) ?? new User("Izabella"))
            },
            {
                Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                new Group (
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),
                    "Victor's study group",
                    "none provided",
                    ApplicationState.Get().UserRepository?.Get(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06")) ?? new User("Victor"))
            }
        };
        public static void CreateGroup(Group group)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                HardcodedGroups.Add(group.Id, group);
                return;
            }
            using SqlConnection connection = new ();
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. HardcodedGroups.Values];
            }

            using SqlConnection connection = new ();
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                UpdateGroupIfDBUnvailable(group);
                return;
            }
            using SqlConnection connection = new ();
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                HardcodedGroups.Remove(id);
                return;
            }
            using SqlConnection connection = new ();
            try
            {
                connection.Open();
            }
            catch (SqlException azureazureTrialExpired)
            {
                Console.WriteLine(azureazureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
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
