using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingCompApplication.Model
{
    public class QuestionRepository : IQuestionRepository
    {
        private static readonly List<QuestionItem> Repo;
        private static int _idIncrement;

        static QuestionRepository()
        {
            _idIncrement = 1;
            Repo = new List<QuestionItem>
            {
                new QuestionItem
                {
                    Id=_idIncrement, QuestionBase64 = "cXVlc3Rpb24/", Question = "question?",
                    Answer = "answer_rerun", Added = DateTime.Now, Ip = "0.0.0.0"
                }
            };
        }

        public IEnumerable<QuestionItem> GetQuestions()
        {
            return Repo;
        }
            
        public QuestionItem AddQuestion(QuestionItem questionItem)
        {
            _idIncrement++;
            questionItem.Id = _idIncrement;
            Repo.Add(questionItem);
            if (Repo.Count > 100){ Repo.RemoveRange(0, Repo.Count-10); }
            return questionItem;
        }

        public QuestionItem RerunQuestion(int id)
        {
            var rerunQuestion = Repo.First(q => q.Id == id);
            rerunQuestion.Answer = GetAnswers.GetAnswer(rerunQuestion.Question);
            return rerunQuestion;
        }
    }
}