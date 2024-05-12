using System.Configuration;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;

namespace Moderation.DbEndpoints
{
    public class JoinRequestAnswerForOneQuestionEndpoints
    {
        private static readonly string ConnectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password=1234567!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static readonly Dictionary<Guid, JoinRequestAnswerToOneQuestion> HardcodedAnswers = new ()
        {
            {
                Guid.Parse("A6006EE8-5D2C-4BB1-9761-334F59982987"),
                new JoinRequestAnswerToOneQuestion(
                    Guid.Parse("A6006EE8-5D2C-4BB1-9761-334F59982987"),
                    Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    "How are you?",
                    "Good. you?")
            },
            {
                Guid.Parse("13F979AC-F705-439C-AEBE-219DC37456FC"),
                new JoinRequestAnswerToOneQuestion(
                    Guid.Parse("13F979AC-F705-439C-AEBE-219DC37456FC"),
                    Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    "When are you free?",
                    "May 1st 2024")
            },
            {
                Guid.Parse("26D6137F-147C-4005-8DDE-16A26511540E"),
                new JoinRequestAnswerToOneQuestion(
                    Guid.Parse("26D6137F-147C-4005-8DDE-16A26511540E"),
                    Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    "Favourite farm animal",
                    "Sheep")
            }
        };
        public static void CreateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                HardcodedAnswers.Add(question.Id, question);
                return;
            }
            string sql = "INSERT INTO JoinRequestMessage VALUES (@JoinRequestId,@[Key], @[Value])";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequestAnswerToOneQuestion> ReadQuestion()
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                return [.. HardcodedAnswers.Values];
            }
            List<JoinRequestAnswerToOneQuestion> allAnswersToAllQuestions = [];

            string sql = "SELECT * FROM JoinRequestMessage";
            using SqlCommand command = new (sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                JoinRequestAnswerToOneQuestion qAndA = new (reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                allAnswersToAllQuestions.Add(qAndA);
            }

            return allAnswersToAllQuestions;
        }
        public static void UpdateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                if (!HardcodedAnswers.ContainsKey(question.Id))
                {
                    return;
                }

                HardcodedAnswers[question.Id] = question;
                return;
            }
            string sql = "UPDATE JoinRequestMessage SET [Value]=@[Value] WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static void DeleteQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new (ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                HardcodedAnswers.Remove(question.Id);
                return;
            }
            string sql = "DELETE FROM JoinRequestMessage WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new (sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.ExecuteNonQuery();
        }
    }
}
