using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class JoinRequestAnswerForOneQuestionRepository : IJoinRequestAnswerForOneQuestionRepository
    {
        protected readonly Dictionary<Guid, JoinRequestAnswerToOneQuestion> data;
        public JoinRequestAnswerForOneQuestionRepository(Dictionary<Guid, JoinRequestAnswerToOneQuestion> data)
        {
            this.data = data;
        }
        public JoinRequestAnswerForOneQuestionRepository() : base()
        {
        }

        public bool Add(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            JoinRequestAnswerForOneQuestionEndpoints.CreateQuestion(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return JoinRequestAnswerForOneQuestionEndpoints.ReadQuestion().Exists(a => a.Id == key);
        }

        public JoinRequestAnswerToOneQuestion? Get(Guid key)
        {
            return JoinRequestAnswerForOneQuestionEndpoints.ReadQuestion().Find(a => a.Id == key);
        }

        public IEnumerable<JoinRequestAnswerToOneQuestion> GetAll()
        {
            return JoinRequestAnswerForOneQuestionEndpoints.ReadQuestion();
        }

        public bool Remove(Guid key)
        {
            return true;
            // throw new Exception("Remove needs more than just the id");
        }
        public static bool Remove(JoinRequestAnswerToOneQuestion question)
        {
            JoinRequestAnswerForOneQuestionEndpoints.DeleteQuestion(question);
            return true;
        }

        public bool Update(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            JoinRequestAnswerForOneQuestionEndpoints.UpdateQuestion(value);
            return true;
        }
    }
}
