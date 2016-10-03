using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using ProgrammingCompApplication.Hubs;
using ProgrammingCompApplication.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgrammingCompApplication.api
{
    public class QuestionsController : Controller
    {
        IQuestionRepository _questionRepository = new QuestionRepository();
        
        [HttpGet("~/api/questions")]
        public async Task<IEnumerable<QuestionItem>> GetAll()
        {
            return await Task.Run(() => _questionRepository.GetQuestions());
        }

        [HttpGet("~/api/questions/rerun/{id}")]
        public async Task<QuestionItem> Rerun(int id)
        {
            // TODO : Use signalR to update question?
            return await Task.Run(() => _questionRepository.RerunQuestion(id));
        }
    }
}
