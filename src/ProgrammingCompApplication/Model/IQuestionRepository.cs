using System.Collections.Generic;

namespace ProgrammingCompApplication.Model
{
    public interface IQuestionRepository
    {
        IEnumerable<QuestionItem> GetQuestions();
        QuestionItem AddQuestion(QuestionItem questionItem);
        QuestionItem RerunQuestion(int id);
    }
}