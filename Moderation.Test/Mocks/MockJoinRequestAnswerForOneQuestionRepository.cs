using Moderation.Entities;
using Backend.Repository.Interfaces;


namespace Moderation.Test.Mocks
{
    internal class MockJoinRequestAnswerForOneQuestionRepository : IJoinRequestAnswerForOneQuestionRepository
    {
        public bool Add(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            return true;
        }

        public bool Contains(Guid key) 
        { 
            return true;
        }

        public JoinRequestAnswerToOneQuestion? Get(Guid key)
        {
            return new JoinRequestAnswerToOneQuestion(key, "Question", "Answer");
        }

        public IEnumerable<JoinRequestAnswerToOneQuestion> GetAll()
        {
            return new List<JoinRequestAnswerToOneQuestion>()
            {
                new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), "Question1", "Answer1"),
                new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), "Question2", "Answer2"),
                new JoinRequestAnswerToOneQuestion(Guid.NewGuid(), "Question3", "Answer3")
            };
        }

        public bool Remove(Guid key)
        {
            return true;
        }


        public bool Update(Guid key, JoinRequestAnswerToOneQuestion value)
        {
            return true;
        }
    }
}
