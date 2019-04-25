using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace TweetsSentimentAnalysis.Services
{
    public class TweetsSearch : ITweetsSearch
    {

        private readonly ILogger<TweetsSearch> _logger;

        public TweetsSearch(ILogger<TweetsSearch> logger)
        {
            _logger = logger;
        }

        public IList<string> GetTweets(string tag)
        {

            _logger.LogInformation(
            "TweetsSearch.GetTweets called. tag: {TAG}",
            tag);

            var searchParameter = Search.CreateTweetSearchParameter("#"+tag);

            
            searchParameter.SearchType = SearchResultType.Popular;
            searchParameter.MaximumNumberOfResults = 200;
            

            var tweets = Search.SearchTweets(searchParameter);

          return  tweets.Select(x => x.FullText).ToList();
        }
    }
}
