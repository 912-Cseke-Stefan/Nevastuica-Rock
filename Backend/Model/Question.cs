using Moderation.Entities;

namespace Moderation.Model
{
    public abstract class Question(string text) : IHasID
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Text { get; } = text;
    }
}