namespace Moderation.Entities
{
    public class Award : IHasID
    {
        public Guid Id { get; set; }
        public enum AwardType
        {
            Bronze,
            Silver,
            Gold
        }
        public AwardType AwardTypeObj { get; set; }

        public Award()
        {
        }

        public Award(Guid id, AwardType awardTypeObj)
        {
            Id = id;
            AwardTypeObj = awardTypeObj;
        }
    }
}
