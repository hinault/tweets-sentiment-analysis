using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TweetsSentimentAnalysis.Models;
using TweetsSentimentAnalysis.Services;

namespace TweetsSentimentAnalysis.Pages
{
    public class IndexModel : PageModel
    {

        public IList<string> Tweets { get; set; }
        public IList<ResultModel> Results { get; set; }

        [Required]
        [StringLength(50)]
        [BindProperty] 
        public string Tag { get; set; }

        private readonly ITweetsSearch _tweetsSearch;
        private readonly ITextAnalyticsService _textAnalyticsService;
        public IndexModel(ITweetsSearch tweetsSearch, ITextAnalyticsService textAnalyticsService)
        {

            _tweetsSearch = tweetsSearch;
            _textAnalyticsService = textAnalyticsService;
        }

        public void OnGet()
        {
            Results = new List<ResultModel>();
        }

        public void OnPost()
        {
            Results = new List<ResultModel>();

            Tweets = _tweetsSearch.GetTweets(Tag);

            if(Tweets.Any())
            { 
            Results = _textAnalyticsService.Language(Tweets).Result;

            Results = _textAnalyticsService.Sentiment(Results).Result;
            }
        }
    }
}
