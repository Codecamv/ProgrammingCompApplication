using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using ProgrammingCompApplication.Hubs;
using ProgrammingCompApplication.Model;

namespace ProgrammingCompApplication.Controllers
{
    public class IncomingController : Controller
    {
        readonly IQuestionRepository _questionRepository = new QuestionRepository();
        private IHttpContextAccessor _accessor;
        private readonly IHubContext _hub;

        public IncomingController(IConnectionManager connectionManager)
        {
            _hub = connectionManager.GetHubContext<QuestionHub>();
        }

        // shows incoming question
        public IActionResult Index()
        {
            return View();
        }

        // handle question
        public async Task<IActionResult> Question(string question)
        {
            var decodedQuestion = "";
            try
            {      
                var decodedQuestionBytes = Convert.FromBase64String(question);
                decodedQuestion = Encoding.UTF8.GetString(decodedQuestionBytes);
            }
            catch (Exception)
            {
                // not base 64 => Use incoming queestion
                decodedQuestion = question;
            }
            var answer = GetAnswers.GetAnswer(decodedQuestion);
            var added = DateTime.Now;
            var ip = "0.0.0.0";
            var newQuestion = new QuestionItem
            {
                QuestionBase64 = question, Question = decodedQuestion,
                Answer = answer, Added = added, Ip = ip
            };
            newQuestion = _questionRepository.AddQuestion(newQuestion);
            _hub.Clients.All.newQuestion(newQuestion);
            return await Task.Run(() => Content(answer));
        }

        [HttpGet]
        [HttpPost]
        public IActionResult CompressString(string question)
        {
            if (string.IsNullOrEmpty(question)) return View(model: "");
            var encodedText = EncodeText(question);
            return View(model:encodedText);
        }

        public static string EncodeText(string question)
        {
            if (string.IsNullOrEmpty(question)) return "";
            var bytesToEncode = Encoding.UTF8.GetBytes(question);
            var encodedText = Convert.ToBase64String(bytesToEncode);
            return encodedText;
        }
    }
}
