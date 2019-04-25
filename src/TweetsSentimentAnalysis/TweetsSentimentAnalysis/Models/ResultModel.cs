using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetsSentimentAnalysis.Models
{
    public class ResultModel
    {
        public string Text { get; set; }
        public string Id { get; set; }
        public string Language { get; set; }
        public string LanguageIso { get; set; }
        public string Score { get; set; }
    }
}
