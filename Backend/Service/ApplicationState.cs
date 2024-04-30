﻿using Backend.Repository;
using Moderation.CurrentSessionNamespace;
using Moderation.Model;
using Moderation.Repository;

namespace Moderation.Serivce
{
    public class ApplicationState
    {
        static private ApplicationState? instance;
        public CurrentSession CurrentSession { get; } = CurrentSession.GetInstance();
        public GroupRepository Groups { get; } = new ();
        public UserRepository UserRepository { get; } = new ();
        public PostRepository Posts { get; } = new ();
        public AwardRepository Awards { get; } = new ();
        public GroupRules Rules { get; } = new ();
        public GroupUserRepository GroupUsers { get; } = new ();
        public JoinRequestAnswerForOneQuestionRepository JoinRequestForOneQuestionAnswers { get; } = new ();
        public JoinRequestRepository JoinRequests { get; } = new ();
        public QuestionRepository Questions { get; } = new ();
        public ReportRepository Reports { get; } = new ();
        public RoleRepository Roles { get; } = new ();
        public TextPostRepository TextPosts { get; } = new ();
        public bool DbConnectionIsAvailable { get; set; } = false;
        static public ApplicationState Get()
        {
            instance ??= new ApplicationState();
            return instance;
        }
    }
}