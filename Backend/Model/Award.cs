﻿namespace Moderation.Entities
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
    }
}
