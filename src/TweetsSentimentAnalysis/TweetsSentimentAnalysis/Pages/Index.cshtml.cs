using System;
using System.Collections.Generic;
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

        public IList<string> Tweets;
        public IList<ResultModel> Results;


        private readonly ITweetsSearch _tweetsSearch;
        private readonly ITextAnalyticsService _textAnalyticsService;
        public IndexModel(ITweetsSearch tweetsSearch, ITextAnalyticsService textAnalyticsService)
        {

            _tweetsSearch = tweetsSearch;
            _textAnalyticsService = textAnalyticsService;
        }

        public void OnGet()
        {
            Tweets = _tweetsSearch.GetTweets("GlobalAzure");
        }
    }
}
