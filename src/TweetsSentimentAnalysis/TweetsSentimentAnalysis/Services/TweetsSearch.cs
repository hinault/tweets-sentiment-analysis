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
            Auth.SetUserCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
            _logger = logger;
        }

        public List<string> GetTweets(string tag)
        {

            _logger.LogInformation(
            "TweetsSearch.GetTweets called. tag: {TAG}",
            tag);

            var searchParameter = Search.CreateTweetSearchParameter("#"+tag);

            
            searchParameter.SearchType = SearchResultType.Popular;
            searchParameter.MaximumNumberOfResults = 100;
            

            var tweets = Search.SearchTweets(searchParameter);

          return  tweets.Select(x => x.FullText).ToList();
        }
    }
}
