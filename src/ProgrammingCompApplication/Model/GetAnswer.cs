using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCalc;

namespace ProgrammingCompApplication.Model
{
    public class GetAnswers
    {
        public static string GetAnswer(string question)
        {
            if (question == "question?")
            {
                return "answer";
            }
            
            //else if (question == "Calculate this formula: 3 + 2")
            //{
            //    return "5";
            //}
            
            //else if (question == "Calculate this formula: 3 + 7")
            //{
            //    return "12";
            //}
            // Q2FsY3VsYXRlIHRoaXMgZm9ybXVsYTogMyArIDI=
            // Q2FsY3VsYXRlIHRoaXMgZm9ybXVsYTogMyArIDc=
            // Q2FsY3VsYXRlIHRoaXMgZm9ybXVsYTogU3FydCg0KQ==
            else if (question.Contains("Calculate this formula:"))
            {
                // https://ncalc.codeplex.com/
                question = question.Replace("Calculate this formula:", "").Replace(" ", "");
                try
                {
                    return new Expression(question).Evaluate().ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }

            return "";

        }
    }
}
