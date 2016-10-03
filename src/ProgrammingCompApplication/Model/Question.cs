using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProgrammingCompApplication.Model
{
    public class QuestionItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("questionBase64")]
        public string QuestionBase64 { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }
        [JsonProperty("added")]
        public DateTime Added { get; set; }
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}
