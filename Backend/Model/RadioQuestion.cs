namespace Moderation.Model
{
    public class RadioQuestion(string text, List<string> options) : Question(text)
    {
        public List<string> Options { get; set; } = options;
    }
}
