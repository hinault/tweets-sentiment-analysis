using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetsSentimentAnalysis.Services
{
    public interface ITweetsSearch
    {
        IList<string> GetTweets(string tag);
    }
}
