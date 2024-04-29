using Moderation.GroupEntryForm;

namespace Moderation.Repository
{
    public class QuestionRepository : Repository<Question>
    {
        public QuestionRepository(Dictionary<Guid, Question> data) : base(data) { }
        public QuestionRepository() : base() { }

        // public IEnumerable<JoinRequestAnswerToOneQuestion> GetQuestionsByGroup(Guid groupId)
        // {
        //    return data.Values.Where(q => q.GroupId == groupId);
        // }
    }
}
