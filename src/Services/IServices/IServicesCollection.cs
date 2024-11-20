namespace thinkschool.OnlineExam.Services.IServices;
public interface IServicesCollection
{
    IApplicationUserService ApplicationUserServices { get; }
    IExamResultService ExamResultServices { get; }
    IExamService ExamServices { get; }
    IOptionService OptionServices { get; }
    IQuestionService QuestionServices { get; }
    ISectionResultService SectionResultServices { get; }
    ISectionService SectionServices { get; }
    IUserAnswerService UserAnswerServices { get; }
    IUserExamService UserExamServices { get; }

    ITokenService TokenService { get; }
}
