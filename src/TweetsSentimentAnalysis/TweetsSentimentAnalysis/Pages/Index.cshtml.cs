using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TweetsSentimentAnalysis.Services;

namespace TweetsSentimentAnalysis.Pages
{
    public class IndexModel : PageModel
    {

        public IList<string> Tweets;
        private ITweetsSearch _tweetsSearch;
        public IndexModel(ITweetsSearch tweetsSearch)
        {

            _tweetsSearch = tweetsSearch;
        }

        public void OnGet()
        {
            Tweets = _tweetsSearch.GetTweets("GlobalAzure");
        }
    }
}
