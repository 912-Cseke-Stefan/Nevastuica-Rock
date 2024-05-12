using Backend.Repository.Interfaces;
using Moderation.CurrentSessionNamespace;

namespace Moderation.Serivce
{
    public class ApplicationState
    {
        private static ApplicationState instance;
        // public CurrentSession CurrentSession { get; } = CurrentSession.GetInstance();
        public IGroupRepository Groups { get; }
        public IUserRepository UserRepository { get; }
        public IPostRepository Posts { get; }
        public IAwardRepository Awards { get; }
        public IGroupRules Rules { get; }
        public IGroupUserRepository GroupUsers { get; }
        public IJoinRequestAnswerForOneQuestionRepository JoinRequestForOneQuestionAnswers { get; }
        public IJoinRequestRepository JoinRequests { get; }
        public IQuestionRepository Questions { get; }
        public IReportRepository Reports { get; }
        public IRoleRepository Roles { get; }
        public ITextPostRepository TextPosts { get; }

        public ApplicationState(IGroupRepository groupRepository,
                                IUserRepository userRepository,
                                IPostRepository postRepository,
                                IAwardRepository awardRepository,
                                IGroupRules groupRules,
                                IGroupUserRepository groupUserRepository,
                                IJoinRequestAnswerForOneQuestionRepository joinRequestAnswerForOneQuestionRepository,
                                IJoinRequestRepository joinRequestRepository,
                                IQuestionRepository questionRepository,
                                IReportRepository reportRepository,
                                IRoleRepository roleRepository,
                                ITextPostRepository textPostRepository)
        {
            Groups = groupRepository;
            UserRepository = userRepository;
            Posts = postRepository;
            Awards = awardRepository;
            Rules = groupRules;
            GroupUsers = groupUserRepository;
            JoinRequestForOneQuestionAnswers = joinRequestAnswerForOneQuestionRepository;
            JoinRequests = joinRequestRepository;
            Questions = questionRepository;
            Reports = reportRepository;
            Roles = roleRepository;
            TextPosts = textPostRepository;

            instance = this;
        }

        public static ApplicationState Get()
        {
            return instance;
        }
    }
}